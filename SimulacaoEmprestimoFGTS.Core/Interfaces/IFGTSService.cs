using SimulacaoEmprestimoFGTS.Core.Dto;
using SimulacaoEmprestimoFGTS.Core.Dto.FGTS;
using SimulacaoEmprestimoFGTS.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Interfaces
{
    public interface IFGTSService
    {
        public IEnumerable<RepasseFGTSDto> GetRepasses(decimal saldo, int parcelas, DateTime dataAniversario);
        public IEnumerable<SimulacaoFGTSDto> GetSimulacaoFGTS(decimal saldo, int parcelas, DateTime dataAniversario, double taxaMensal, DateTime dataSimulacao);
        public ResumoFGTSDto GetSimulacaoResumoFGTS(decimal saldo, int parcelas, DateTime dataAniversario, double taxaMensal, DateTime dataSimulacao);
    }
}
