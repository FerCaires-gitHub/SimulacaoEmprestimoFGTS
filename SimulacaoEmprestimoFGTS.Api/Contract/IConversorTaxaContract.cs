using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacaoEmprestimoFGTS.Api.Contract
{
    public interface IConversorTaxaContract
    {
        public IActionResult MensalToAnual(double taxaMensal);
        public IActionResult MensalToDiaria(double taxaMensal);

        public IActionResult AnualToDiaria(double taxaAnual);
        public IActionResult AnualToMensal(double taxaAnual);

        public IActionResult DiariaToMensal(double taxaDiaria);
        public IActionResult DiariaToAnual(double taxaDiaria);
    }
}
