using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Dto.BASE
{
    public class SimulacaoBaseDto
    {
        public int Parcela { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorAmortizacao { get; set; }
    }
}
