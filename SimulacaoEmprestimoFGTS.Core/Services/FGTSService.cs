using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimulacaoEmprestimoFGTS.Core.Dto;
using SimulacaoEmprestimoFGTS.Core.Dto.FGTS;
using SimulacaoEmprestimoFGTS.Core.Interfaces;
using SimulacaoEmprestimoFGTS.Domain.Model;
using SimulacaoEmprestimoFGTS.Domain.Model.CONFIG;
using SimulacaoEmprestimoFGTS.Domain.Model.FGTS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SimulacaoEmprestimoFGTS.Core.Services
{
    public class FGTSService : IFGTSService
    {
        private readonly IConversorTaxaService _conversorTaxa;
        private readonly IIOFService _iOFService;
        private readonly IMapper _mapper;
        private readonly ILogger<FGTSService> _logger;
        private readonly IOptions<AppConfig> _options;
        private readonly IEnumerable<AliquotaFGTS> _aliquotasFGTS;
        public FGTSService(IConversorTaxaService conversorTaxa, IIOFService iOFService, IMapper mapper, ILogger<FGTSService> logger, IOptions<AppConfig> options)
        {
            _conversorTaxa = conversorTaxa;
            _iOFService = iOFService;
            _mapper = mapper;
            _logger = logger;
            _options = options;
            _aliquotasFGTS = AliquotaFGTS.GetAliquotasFGTS();
        }
        private void ValidaNumeroParcelas(int parcelas)
        {
            var minimo = _options.Value.FGTSConfig.MinimoParcela;
            var maximo = _options.Value.FGTSConfig.MaximoParcela;
            if (parcelas < minimo || parcelas > maximo)
                throw new Exception($"Número de parcelas informado não permitido. Min:{minimo} - Max:{maximo}");
        }
        public IEnumerable<RepasseFGTSDto> GetRepasses(decimal saldo, int parcelas, DateTime dataAniversario)
        {
            ValidaNumeroParcelas(parcelas);
            var sw = Stopwatch.StartNew();
            var repasses = new List<RepasseFGTS>();
            for (int i = 0; i < parcelas; i++)
            {
                DateTime dataVencimento = CalculaDataVencimento(dataAniversario, DateTime.Now);
                var aliquota = GetAliquotaFGTS(saldo);
                var repasse = CalculaRepasse(aliquota, saldo, dataVencimento.AddYears(i));
                saldo = CalculaSaldoRestante(saldo, repasse);
                repasses.Add(repasse);
            }

            sw.Stop();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} - Tempo gasto:{sw.ElapsedMilliseconds} ms ");
            return repasses.Select(x =>_mapper.Map<RepasseFGTSDto>(x));
        }
        private DateTime CalculaDataVencimento(DateTime dataAniversario, DateTime dataSimulacao)
        {
            if(dataAniversario.Month > dataSimulacao.Month)
                return new DateTime(DateTime.Now.Year, dataAniversario.Month, 1);
            else
                return new DateTime(DateTime.Now.Year+1, dataAniversario.Month, 1);
        }
        public IEnumerable<SimulacaoFGTSDto> GetSimulacaoFGTS(decimal saldo, int parcelas, DateTime dataAniversario, double taxaMensal, DateTime dataSimulacao)
        {
            ValidaNumeroParcelas(parcelas);
            var simulacoes = new List<SimulacaoFGTS>();
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < parcelas; i++)
            {
                DateTime dataVencimento = CalculaDataVencimento(dataAniversario,dataSimulacao).AddYears(i);
                var dias = Math.Abs(dataSimulacao.Subtract(dataVencimento).Days);
                var aliquota = GetAliquotaFGTS(saldo);
                var repasse = CalculaRepasse(aliquota, saldo, dataVencimento);
                var taxaJuros = CalculaTaxaOperacao(_conversorTaxa.MensalToDiaria(taxaMensal),dias );
                var principal = Math.Round(CalculaPrincipal(repasse.ValorParcela, taxaJuros),2);
                var juros = CalculaJuros(repasse.ValorParcela, principal);
                var iof = Math.Round(CalculaIOF(principal, _iOFService.GetAliquotaIOF(dataSimulacao,dias)),2);
                var valorLiberado = CalculaValorLiberado(principal, iof);
                simulacoes.Add(new SimulacaoFGTS {IOF = iof, Juros = juros, Principal = principal, ValorLiberado = valorLiberado, Repasse = repasse });
                saldo = CalculaSaldoRestante(saldo, repasse);
            }
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} - Tempo gasto:{sw.ElapsedMilliseconds} ms ");
            return simulacoes.Select(x => _mapper.Map<SimulacaoFGTSDto>(x));
        }
        private AliquotaFGTS GetAliquotaFGTS(decimal saldo)
        {
            return _aliquotasFGTS.FirstOrDefault(x => saldo >= x.Minimo && saldo <= x.Maximo);
        }
        private RepasseFGTS CalculaRepasse(AliquotaFGTS aliquota, decimal saldo, DateTime DataVencimento)
        {
            return new RepasseFGTS { ValorParcela = saldo * aliquota.Percentual/100 + aliquota.ParcelaAdicional, Aliquota = aliquota, DataVencimento = DataVencimento };
        }
        private decimal CalculaSaldoRestante(decimal saldo, RepasseFGTS repasse)
        {
            var saldoFinal = saldo - repasse.ValorParcela;
            return saldoFinal < 0 ? 0 : saldoFinal;
        }
        private double CalculaTaxaOperacao(double taxaDiaria, int dias)
        {
            return Math.Pow(1 + taxaDiaria, dias);
        }
        private decimal CalculaPrincipal(decimal valorParcela, double taxaOperacao)
        {
            return valorParcela / Convert.ToDecimal(Math.Round(taxaOperacao,6));
        }
        private decimal CalculaJuros(decimal valorParcela, decimal valorPrincipal)
        {
            return valorParcela - valorPrincipal;
        }
        private decimal CalculaIOF(decimal valorPrincipal, double aliquotaIOF)
        {
            return valorPrincipal * Convert.ToDecimal(Math.Round(aliquotaIOF,6));
        }
        private decimal CalculaValorLiberado(decimal valorPrincipal, decimal valorIof)
        {
            return valorPrincipal - valorIof;
        }
        public ResumoFGTSDto GetSimulacaoResumoFGTS(decimal saldo, int parcelas, DateTime dataAniversario, double taxaMensal, DateTime dataSimulacao)
        {
            ValidaNumeroParcelas(parcelas);
            var sw = Stopwatch.StartNew();
            var simulacao = GetSimulacaoFGTS(saldo, parcelas, dataAniversario, taxaMensal, dataSimulacao);
            var valorTotal = simulacao.Sum(x => x.ValorRepasseFGTS);
            var valorLiberado = simulacao.Sum(x => x.ValorLiberado);
            var valorFinanciado = simulacao.Sum(x => x.Principal);
            var valorJuros = simulacao.Sum(x => x.Juros);
            var valorIOF = simulacao.Sum(x => x.IOF);
            var dataInicio = simulacao.Min(x => x.DataVencimento);
            var dataFinal = simulacao.Max(x => x.DataVencimento);
            sw.Stop();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} - Tempo gasto:{sw.ElapsedMilliseconds} ms ");
            return new ResumoFGTSDto
            {
                DataFim = dataFinal,
                DataInicio = dataInicio,
                IOF = valorIOF, 
                Juros = valorJuros, 
                Solicitado = valorFinanciado, 
                TaxaMensal = taxaMensal, 
                ValorLiberado = valorLiberado,
                ValorOperacao = valorTotal
                
            };
        }
    }
}
