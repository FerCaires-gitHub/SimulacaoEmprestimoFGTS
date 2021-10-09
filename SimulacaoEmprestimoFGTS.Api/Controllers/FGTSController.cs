using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimulacaoEmprestimoFGTS.Api.Contract;
using SimulacaoEmprestimoFGTS.Core.Dto;
using SimulacaoEmprestimoFGTS.Core.Dto.FGTS;
using SimulacaoEmprestimoFGTS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacaoEmprestimoFGTS.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FGTSController : ControllerBase, IFGTSContract
    {
        private readonly IFGTSService _service;
        private readonly ILogger<FGTSController> _logger;

        public FGTSController(IFGTSService service, ILogger<FGTSController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [Route("Repasses")]
        [HttpGet]
        public ActionResult<IEnumerable<RepasseFGTSDto>> GetRepassesFGTS(decimal saldo, int parcelas, DateTime dataAniversario)
        {
            try
            {
                _logger.LogInformation($"Repasse utilizando os valores - Saldo:{saldo}, Parcelas:{parcelas}, DataAniversario:{dataAniversario}");
                var repasses = _service.GetRepasses(saldo, parcelas, dataAniversario);
                return Ok(repasses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao calcular repasses: {ex.Message}");
                return BadRequest("Erro ao calcular repasses");
            }
        }
        
        [Route("SimulacaoResumo")]
        [HttpGet]
        public ActionResult<ResumoFGTSDto> GetResumoSimulacaoFGTS(decimal saldo, int parcelas, DateTime dataAniversario, double taxaMensal, DateTime dataSimulacao)
        {
            try
            {
                _logger.LogInformation($"Resumo simulação utilizando os valores - Saldo:{saldo}, Parcelas:{parcelas}, DataAniversario:{dataAniversario}, Taxa Mensal:{taxaMensal}, Data Simulação:{dataSimulacao}");
                var resumo = _service.GetSimulacaoResumoFGTS(saldo, parcelas, dataAniversario, taxaMensal, dataSimulacao);
                return Ok(resumo);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Erro ao calcular resumo simulação FGTS: {ex.Message}");
                return BadRequest("Erro ao calcular resumo simulação FGTS");
            }
        }

        [Route("SimulacaoDetalhada")]
        [HttpGet]
        public ActionResult<IEnumerable<SimulacaoFGTSDto>> GetSimulacaoFGTS(decimal saldo, int parcelas, DateTime dataAniversario, double taxaMensal, DateTime dataSimulacao)
        {
            try
            {
                _logger.LogInformation($"Detalhamento simulação utilizando os valores - Saldo:{saldo}, Parcelas:{parcelas}, DataAniversario:{dataAniversario}, Taxa Mensal:{taxaMensal}, Data Simulação:{dataSimulacao}");
                var simulacoes = _service.GetSimulacaoFGTS(saldo, parcelas, dataAniversario, taxaMensal, dataSimulacao);
                return Ok(simulacoes);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Erro ao calcular simulação FGTS: {ex.Message}");
                return BadRequest("Erro ao calcular simulação FGTS");
            }
        }
    }
}
