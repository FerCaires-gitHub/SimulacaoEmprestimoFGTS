using SimulacaoEmprestimoFGTS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Services
{
    public class ConversorTaxaService : IConversorTaxaService
    {

        public double AnualToDiaria(double taxaAnual)
        {
            return Math.Pow(1 + taxaAnual/100, 1.0 / 365)-1;
        }

        public double AnualToMensal(double taxaAnual)
        {
            return Math.Pow(1 +taxaAnual/100, 1.0 / 12)-1;
        }

        public double DiariaToAnual(double taxaDiaria)
        {
            return Math.Pow(1 + taxaDiaria/100, 365)-1;
        }

        public double DiariaToMensal(double taxaDiaria)
        {
            return Math.Pow(1 + taxaDiaria/100, 30)-1;
        }

        public double MensalToAnual(double taxaMensal)
        {
            return Math.Pow(1 + taxaMensal/100, 12)-1;
        }

        public double MensalToDiaria(double taxaMensal)
        {
            return Math.Pow(1 + taxaMensal/100, 1.0 / 30)-1;
        }
    }
}
