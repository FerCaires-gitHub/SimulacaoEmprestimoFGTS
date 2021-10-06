using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Interfaces
{
    public interface IConversorTaxaService
    {
        public double MensalToAnual(double taxaMensal);
        public double MensalToDiaria(double taxaMensal);

        public double AnualToDiaria(double taxaAnual);
        public double AnualToMensal(double taxaAnual);

        public double DiariaToMensal(double taxaDiaria);
        public double DiariaToAnual(double taxaDiaria);
    }
}
