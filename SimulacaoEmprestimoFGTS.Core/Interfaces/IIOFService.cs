using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Interfaces
{
    public interface IIOFService
    {
        public double GetAliquotaIOF(DateTime dataSimulacao,int dias);
    }
}
