using Microsoft.AspNetCore.Mvc;
using SimulacaoEmprestimoFGTS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacaoEmprestimoFGTS.Api.Contract
{
    public interface IFGTSContract
    {
        public IActionResult GetRepassesFGTS(decimal saldo, int parcelas, DateTime dataAniversario);

        public IActionResult GetSimulacaoFGTS(decimal saldo, int parcelas, DateTime dataAniversario, double taxaMensal, DateTime dataSimulacao);
    }
}
