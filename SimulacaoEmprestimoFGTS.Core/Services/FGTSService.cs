using AutoMapper;
using Microsoft.Extensions.Logging;
using SimulacaoEmprestimoFGTS.Core.Dto;
using SimulacaoEmprestimoFGTS.Core.Interfaces;
using SimulacaoEmprestimoFGTS.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimulacaoEmprestimoFGTS.Core.Services
{
    public class FGTSService : IFGTSService
    {
        private readonly IConversorTaxaService _conversorTaxa;
        private readonly IIOFService _iOFService;
        private readonly IMapper _mapper;
        private readonly ILogger<FGTSService> _logger;
        private readonly IEnumerable<AliquotaFGTS> _aliquotasFGTS;
        public FGTSService(IConversorTaxaService conversorTaxa, IIOFService iOFService, IMapper mapper, ILogger<FGTSService> logger)
        {
            _conversorTaxa = conversorTaxa;
            _iOFService = iOFService;
            _mapper = mapper;
            _logger = logger;
            _aliquotasFGTS = AliquotaFGTS.GetAliquotasFGTS();
        }
        public IEnumerable<RepasseFGTSDto> GetRepasses(decimal saldo, int parcelas, DateTime dataAniversario)
        {
            var repasses = new List<RepasseFGTS>();
            for (int i = 1; i <= parcelas; i++)
            {
                DateTime dataVencimento = CalculaDataVencimento(dataAniversario);
                var aliquota = GetAliquotaFGTS(saldo);
                var repasse = CalculaRepasse(aliquota, saldo, dataVencimento.AddYears(i));
                saldo = CalculaSaldoRestante(saldo, repasse);
                repasses.Add(repasse);
            }

            return repasses.Select(x =>_mapper.Map<RepasseFGTSDto>(x));
        }

        private DateTime CalculaDataVencimento(DateTime dataAniversario)
        {
            if(dataAniversario > DateTime.Now)
                return new DateTime(DateTime.Now.Year, dataAniversario.Month, 1);
            else
                return new DateTime(DateTime.Now.Year+1, dataAniversario.Month, 1);
        }

        public IEnumerable<SimulacaoFGTSDto> GetSimulacaoFGTS(decimal saldo, int parcelas, DateTime dataAniversario, double taxaMensal, DateTime dataSimulacao)
        {
            var simulacoes = new List<SimulacaoFGTS>();
            for (int i = 0; i < parcelas; i++)
            {
                DateTime dataVencimento = CalculaDataVencimento(dataAniversario).AddYears(i);
                var dias = Math.Abs(dataSimulacao.Subtract(dataVencimento).Days);
                var aliquota = GetAliquotaFGTS(saldo);
                var repasse = CalculaRepasse(aliquota, saldo, dataVencimento.AddYears(i));
                var taxaJuros = CalculaTaxaOperacao(_conversorTaxa.MensalToDiaria(taxaMensal),dias );
                var principal = Math.Round(CalculaPrincipal(repasse.ValorParcela, taxaJuros),2);
                var juros = CalculaJuros(repasse.ValorParcela, principal);
                var iof = Math.Round(CalculaIOF(principal, _iOFService.GetAliquotaIOF(dataSimulacao,dias)),2);
                var valorLiberado = CalculaValorLiberado(principal, iof);
                simulacoes.Add(new SimulacaoFGTS {IOF = iof, Juros = juros, Principal = principal, ValorLiberado = valorLiberado, Repasse = repasse });
                saldo = CalculaSaldoRestante(saldo, repasse);
            }

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


    }
}
