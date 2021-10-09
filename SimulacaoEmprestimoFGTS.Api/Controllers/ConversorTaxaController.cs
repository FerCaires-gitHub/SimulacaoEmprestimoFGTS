using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimulacaoEmprestimoFGTS.Api.Contract;
using SimulacaoEmprestimoFGTS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacaoEmprestimoFGTS.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConversorTaxaController:ControllerBase, IConversorTaxaContract
    {
        private readonly ILogger<ConversorTaxaController> _logger;
        private readonly IConversorTaxaService _service;

        public ConversorTaxaController(ILogger<ConversorTaxaController> logger,IConversorTaxaService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("AnualToDiaria")]
        [HttpGet]
        public IActionResult AnualToDiaria(double taxaAnual)
        {
            try
            {
                var taxaSaida = _service.AnualToDiaria(taxaAnual)*100;
                return Ok(taxaSaida);
                   
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao calcular taxa:{ex.Message}");
                return BadRequest("Erro ao calculara taxa");
            }
        }

        [Route("AnualToMensal")]
        [HttpGet]
        public IActionResult AnualToMensal(double taxaAnual)
        {
            try
            {
                var taxaSaida = _service.AnualToMensal(taxaAnual) * 100;
                return Ok(taxaSaida);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao calcular taxa:{ex.Message}");
                return BadRequest("Erro ao calculara taxa");
            }
        }

        [Route("DiariaToAnual")]
        [HttpGet]
        public IActionResult DiariaToAnual(double taxaDiaria)
        {
            try
            {
                var taxaSaida = _service.DiariaToAnual(taxaDiaria) * 100;
                return Ok(taxaSaida);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao calcular taxa:{ex.Message}");
                return BadRequest("Erro ao calculara taxa");
            }
        }
        [Route("DiariaToMensal")]
        [HttpGet]

        public IActionResult DiariaToMensal(double taxaDiaria)
        {
            try
            {
                var taxaSaida = _service.DiariaToMensal(taxaDiaria) * 100;
                return Ok(taxaSaida);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao calcular taxa:{ex.Message}");
                return BadRequest("Erro ao calculara taxa");
            }
        }
        [Route("MensalToAnual")]
        [HttpGet]
        public IActionResult MensalToAnual(double taxaMensal)
        {
            try
            {
                var taxaSaida = _service.MensalToAnual(taxaMensal) * 100;
                return Ok(taxaSaida);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao calcular taxa:{ex.Message}");
                return BadRequest("Erro ao calculara taxa");
            }
        }

        [Route("MensalToDiaria")]
        [HttpGet]
        public IActionResult MensalToDiaria(double taxaMensal)
        {
            try
            {
                var taxaSaida = _service.MensalToDiaria(taxaMensal) * 100;
                return Ok(taxaSaida);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao calcular taxa:{ex.Message}");
                return BadRequest("Erro ao calculara taxa");
            }
        }
    }
}
