using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimulacaoEmprestimoFGTS.Api.Contract;
using SimulacaoEmprestimoFGTS.Core.Dto;
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

        [Route("GetRepasses")]
        [HttpGet]
        public IActionResult GetRepassesFGTS(decimal saldo, int parcelas, DateTime dataAniversario)
        {
            _logger.LogInformation($"Repasse utilizando os valores - Saldo:{saldo}, Parcelas:{parcelas}, DataAniversario:{dataAniversario}");
            var repasses = _service.GetRepasses(saldo, parcelas, dataAniversario);
            return Ok(repasses);
        }
        [Route("GetSimulacao")]
        [HttpGet]
        public IActionResult GetSimulacaoFGTS(decimal saldo, int parcelas, DateTime dataAniversario, double taxaMensal, DateTime dataSimulacao)
        {
            _logger.LogInformation($"Simulação utilizando os valores - Saldo:{saldo}, Parcelas:{parcelas}, DataAniversario:{dataAniversario}, Taxa Mensal:{taxaMensal}, Data Simulação:{dataSimulacao}");
            var simulacoes = _service.GetSimulacaoFGTS(saldo, parcelas, dataAniversario, taxaMensal, dataSimulacao);
            return Ok(simulacoes);
        }
    }
}
