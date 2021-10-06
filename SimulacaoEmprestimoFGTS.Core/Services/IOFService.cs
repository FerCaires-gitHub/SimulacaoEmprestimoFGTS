using Microsoft.Extensions.Logging;
using SimulacaoEmprestimoFGTS.Core.Interfaces;
using SimulacaoEmprestimoFGTS.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Services
{
    public class IOFService : IIOFService
    {
        private readonly ILogger<IOFService> _logger;
        private  AliquotaIOF _aliquotaIOF;
        public IOFService(ILogger<IOFService> logger)
        {
            _logger = logger;
        }
        public double GetAliquotaIOF(DateTime dataSimulacao, int dias)
        {
            _aliquotaIOF = AliquotaIOF.GetAliquotaIOF(dataSimulacao);
            var aliquota = _aliquotaIOF.Mensal * dias;
            return aliquota >= _aliquotaIOF.Anual ? _aliquotaIOF.Anual : aliquota;
        }
    }
}
