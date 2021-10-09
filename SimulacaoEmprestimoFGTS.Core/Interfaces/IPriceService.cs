using SimulacaoEmprestimoFGTS.Core.Dto.PRICE;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Interfaces
{
    public interface IPriceService
    {
        public IEnumerable<SimulacaoPriceDto> GetSimulacaoPriceDetalhadaEmprestimo(decimal valorEmprestimo, double taxaMensal, int prazo);
        public IEnumerable<SimulacaoPriceDto> GetSimulacaoPriceDetalhadaParcela(decimal valorParcela, double taxaMensal, int prazo);
        public ResumoPriceDto GetSimulacaoPriceResumidaEmprestimo(decimal valorEmprestimo, double taxaMensal, int prazo);
        public ResumoPriceDto GetSimulacaoPriceResumidaParcela(decimal valorParcela, double taxaMensal, int prazo);
    }
}
