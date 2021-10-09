using Microsoft.AspNetCore.Mvc;
using SimulacaoEmprestimoFGTS.Core.Dto;
using SimulacaoEmprestimoFGTS.Core.Dto.FGTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacaoEmprestimoFGTS.Api.Contract
{
    public interface IFGTSContract
    {
        public ActionResult<IEnumerable<RepasseFGTSDto>> GetRepassesFGTS(decimal saldo, int parcelas, DateTime dataAniversario);

        public ActionResult<IEnumerable<SimulacaoFGTSDto>> GetSimulacaoFGTS(decimal saldo, int parcelas, DateTime dataAniversario, double taxaMensal, DateTime dataSimulacao);

        public ActionResult<ResumoFGTSDto> GetResumoSimulacaoFGTS(decimal saldo, int parcelas, DateTime dataAniversario, double taxaMensal, DateTime dataSimulacao);

    }
}
