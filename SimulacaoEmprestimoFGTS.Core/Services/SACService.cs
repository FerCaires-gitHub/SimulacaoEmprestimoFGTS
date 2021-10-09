using AutoMapper;
using Microsoft.Extensions.Logging;
using SimulacaoEmprestimoFGTS.Core.Dto.SAC;
using SimulacaoEmprestimoFGTS.Core.Interfaces;
using SimulacaoEmprestimoFGTS.Domain.Model.SAC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Services
{
    public class SACService : ISACService
    {
        private readonly ILogger<SACService> _logger;
        private readonly IMapper _mapper;
        private readonly ICalculosService _calculosService;

        public SACService(ILogger<SACService> logger, IMapper mapper , ICalculosService calculosService)
        {
            _logger = logger;
            _mapper = mapper;
            _calculosService = calculosService;
        }
        public IEnumerable<SimulacaoSACDto> GetSimulacaoDetalhada(decimal valorEmprestimo, double taxaMensal, int prazo)
        {
            if (prazo <= 0)
                throw new Exception("Prazo menor ou igual a zero");
            try
            {
                var sw = Stopwatch.StartNew();
                var parcelas = new List<SimulacaoSAC>();
                var valorAmortizacao = Math.Round(valorEmprestimo / prazo, 2);
                var saldoDevedor = valorEmprestimo;
                var taxaOperacao = _calculosService.CalculaTaxaOperacao(taxaMensal / 100, 1);
                for (int i = 0; i < prazo; i++)
                {
                    var juros = _calculosService.CalculaValorJuros(taxaOperacao, saldoDevedor);
                    var valorParcela = valorAmortizacao + juros;
                    saldoDevedor -= valorAmortizacao;
                    parcelas.Add(new SimulacaoSAC { Parcela = i + 1, SaldoDevedor = saldoDevedor, ValorAmortizacao = valorAmortizacao, ValorJuros = juros, ValorParcela = valorParcela });
                }
                sw.Stop();
                _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} - Tempo gasto: {sw.ElapsedMilliseconds} ms ");
                return parcelas.Select(x => _mapper.Map<SimulacaoSACDto>(x));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro Simulação SAC: {ex.Message}");
                throw ex;
            }
        }

        public ResumoSACDto GetSimulacaoResumida(decimal valorEmprestimo, double taxaMensal, int prazo)
        {
            var sw = Stopwatch.StartNew();
            var simulacao = GetSimulacaoDetalhada(valorEmprestimo, taxaMensal, prazo);
            var valorTotal = simulacao.Sum(x => x.ValorParcela);
            var valorJuros = simulacao.Sum(x => x.ValorJuros);
            sw.Stop();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} - Tempo gasto: {sw.ElapsedMilliseconds} ms ");
            return new ResumoSACDto { Juros = valorJuros, ValorFinanciado = valorEmprestimo, Tempo = prazo, ValorTotal = valorTotal };
        }
    }
}
