using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using SimulacaoEmprestimoFGTS.Api.Contract;
using SimulacaoEmprestimoFGTS.Core.Dto.SAC;
using SimulacaoEmprestimoFGTS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacaoEmprestimoFGTS.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SACController : ControllerBase, ISACContract
    {
        private readonly ILogger<SACController> _logger;
        private readonly ISACService _service;

        public SACController(ILogger<SACController> logger, ISACService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("ResumoSimulacao")]
        [HttpGet]
        public ActionResult<ResumoSACDto> GetResumoSimulacaoSAC(decimal valorEmprestimo, double taxaMensal, int prazo)
        {
            try
            {
                _logger.LogInformation($"Resumo SAC - ValorEmprestimo:{valorEmprestimo}, TaxaMensal:{taxaMensal}, Prazo:{prazo}");
                var resumo = _service.GetSimulacaoResumida(valorEmprestimo, taxaMensal, prazo);
                return Ok(resumo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar simulação resumida: {ex.Message}");
                return BadRequest("Erro ao processar simulação resumida");
            }
        }


        [Route("SimulacaoDetalhada")]
        [HttpGet]
        public ActionResult<IEnumerable<SimulacaoSACDto>> GetSimulacaoSAC(decimal valorEmprestimo, double taxaMensal, int prazo)
        {
            try
            {
                var simulacao = _service.GetSimulacaoDetalhada(valorEmprestimo, taxaMensal, prazo);
                return Ok(simulacao);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar simulação detalhada: {ex.Message}");
                return BadRequest("Erro ao processar simulação detalhada");
            }
        }
    }
}
