using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Interfaces
{
    public interface ICalculosService
    {
        public double CalculaTaxaOperacao(double taxa, int periodo);
        public decimal CalculaValorJuros(double taxaPeriodo, decimal valor);
        public decimal CalculaValorPresente(decimal valorFuturo, double taxaPeriodo);
    }
}
