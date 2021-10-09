using SimulacaoEmprestimoFGTS.Core.Dto.SAC;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Interfaces
{
    public interface ISACService
    {
        public IEnumerable<SimulacaoSACDto> GetSimulacaoDetalhada(decimal valorEmprestimo, double taxaMensal, int prazo);
        public ResumoSACDto GetSimulacaoResumida(decimal valorEmprestimo, double taxaMensal, int prazo);
    }
}
