using Microsoft.AspNetCore.Mvc;
using SimulacaoEmprestimoFGTS.Core.Dto.PRICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacaoEmprestimoFGTS.Api.Contract
{
    public interface IPRICEContract
    {
        public ActionResult<IEnumerable<SimulacaoPriceDto>> GetSimulacaoPriceDetalhadaEmprestimo(decimal valorEmprestimo, double taxaMensal, int prazo);
        public ActionResult<IEnumerable<SimulacaoPriceDto>> GetSimulacaoPriceDetalhadaParcela(decimal valorParcela, double taxaMensal, int prazo);
        public ActionResult<ResumoPriceDto> GetSimulacaoPriceResumidaEmprestimo(decimal valorEmprestimo, double taxaMensal, int prazo);
        public ActionResult<ResumoPriceDto> GetSimulacaoPriceResumidaParcela(decimal valorParcela, double taxaMensal, int prazo);
    }                 
}
