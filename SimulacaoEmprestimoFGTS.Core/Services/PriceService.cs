using Microsoft.Extensions.Logging;
using SimulacaoEmprestimoFGTS.Core.Dto.PRICE;
using SimulacaoEmprestimoFGTS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Services
{
    public class PriceService : IPriceService
    {
        private readonly ILogger<PriceService> _logger;
        private readonly ICalculosService _calculosService;

        public PriceService(ILogger<PriceService> logger, ICalculosService calculosService)
        {
            _logger = logger;
            _calculosService = calculosService;
        }
        public IEnumerable<SimulacaoPriceDto> GetSimulacaoPriceDetalhadaEmprestimo(decimal valorEmprestimo, double taxaMensal, int prazo)
        {
            var sw = Stopwatch.StartNew();
            var simulacao = new List<SimulacaoPriceDto>();
            var valorParcela = CalculaValorParcela(valorEmprestimo, taxaMensal, prazo);
            var saldoDevedor = valorEmprestimo;
            var taxaOperacao = _calculosService.CalculaTaxaOperacao(taxaMensal/100, 1);
            for (int i = 0; i < prazo; i++)
            {
                var juros = _calculosService.CalculaValorJuros(taxaOperacao, saldoDevedor);
                var amortizacao = valorParcela - juros;
                saldoDevedor -= amortizacao;
                simulacao.Add(new SimulacaoPriceDto { Parcela = i+1, ValorAmortizacao = amortizacao, ValorJuros = juros, ValorParcela = valorParcela });
            }
            sw.Stop();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} - Tempo gasto:{sw.ElapsedMilliseconds} ms");
            return simulacao;
            
        }

        public IEnumerable<SimulacaoPriceDto> GetSimulacaoPriceDetalhadaParcela(decimal valorParcela, double taxaMensal, int prazo)
        {
            var sw = Stopwatch.StartNew();
            var simulacao = new List<SimulacaoPriceDto>();
            var valorEmprestimo = CalculaValorEmprestimo(valorParcela, taxaMensal, prazo);
            var saldoDevedor = valorEmprestimo;
            var taxaOperacao = _calculosService.CalculaTaxaOperacao(taxaMensal/100, 1);
            for (int i = 0; i < prazo; i++)
            {
                var juros = _calculosService.CalculaValorJuros(taxaOperacao, saldoDevedor);
                var amortizacao = valorParcela - juros;
                saldoDevedor -= amortizacao;
                simulacao.Add(new SimulacaoPriceDto { Parcela = i + 1, ValorAmortizacao = amortizacao, ValorJuros = juros, ValorParcela = valorParcela });
            }
            sw.Stop();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} - Tempo gasto:{sw.ElapsedMilliseconds} ms");
            return simulacao;
        }

        public ResumoPriceDto GetSimulacaoPriceResumidaEmprestimo(decimal valorEmprestimo, double taxaMensal, int prazo)
        {
            var sw = Stopwatch.StartNew();
            var simulacao = GetSimulacaoPriceDetalhadaEmprestimo(valorEmprestimo, taxaMensal, prazo);
            var valorTotal = simulacao.Sum(x => x.ValorParcela);
            var juros = simulacao.Sum(x => x.ValorJuros);
            sw.Stop();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} - Tempo gasto:{sw.ElapsedMilliseconds} ms");
            return new ResumoPriceDto { Juros = juros, Tempo = prazo, ValorFinanciado = valorEmprestimo, ValorTotal = valorTotal };
        }

        public ResumoPriceDto GetSimulacaoPriceResumidaParcela(decimal valorParcela, double taxaMensal, int prazo)
        {
            var sw = Stopwatch.StartNew();
            var simulacao = GetSimulacaoPriceDetalhadaParcela(valorParcela, taxaMensal, prazo);
            var valorTotal = simulacao.Sum(x => x.ValorParcela);
            var juros = simulacao.Sum(x => x.ValorJuros);
            var valorEmprestimo = simulacao.Sum(x => x.ValorAmortizacao);
            sw.Stop();
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} - Tempo gasto:{sw.ElapsedMilliseconds} ms");
            return new ResumoPriceDto { Juros = juros, Tempo = prazo, ValorFinanciado = valorEmprestimo, ValorTotal = valorTotal };
        }


        private decimal CalculaValorParcela(decimal valorEmprestimo, double taxaMensal, int prazo)
        {
            var taxa = Math.Round(taxaMensal / 100, 6);
            var elementoComum = Convert.ToDecimal(Math.Pow(1 + taxa, prazo));
            var numerador = elementoComum * Convert.ToDecimal(taxa);
            var denominador = elementoComum - 1;
            return Math.Round(valorEmprestimo * numerador / denominador,2);
        }

        private decimal CalculaValorEmprestimo(decimal valorParcela, double taxaMensal, int prazo)
        {
            var taxa = Math.Round(taxaMensal / 100, 6);
            var elementoComum = Convert.ToDecimal(Math.Pow(1 + taxa, prazo));
            var denominador = elementoComum * Convert.ToDecimal(taxa);
            var numerador = elementoComum - 1;
            return Math.Round(valorParcela * numerador / denominador,2);
        }


    }
}
