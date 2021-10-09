using SimulacaoEmprestimoFGTS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Services
{
    public class CalculosService : ICalculosService
    {
        public CalculosService()
        {

        }
        public double CalculaTaxaOperacao(double taxa, int periodo)
        {
            return Math.Pow(1 + taxa, periodo) - 1;
        }

        public decimal CalculaValorJuros(double taxaPeriodo, decimal valor)
        {
            return Math.Round(valor * Convert.ToDecimal(Math.Round(taxaPeriodo, 6)),2);
        }

        public decimal CalculaValorPresente(decimal valorFuturo, double taxaPeriodo)
        {
            throw new NotImplementedException();
        }
    }
}
