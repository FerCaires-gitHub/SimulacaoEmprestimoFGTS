using Microsoft.AspNetCore.Mvc;
using SimulacaoEmprestimoFGTS.Core.Dto.SAC;
using System.Collections.Generic;

namespace SimulacaoEmprestimoFGTS.Api.Contract
{
    public interface ISACContract
    {        
        public ActionResult<IEnumerable<SimulacaoSACDto>> GetSimulacaoSAC(decimal valorEmprestimo,double taxaMensal, int prazo);
        public ActionResult<ResumoSACDto> GetResumoSimulacaoSAC(decimal valorEmprestimo, double taxaMensal, int prazo);
    }
}
