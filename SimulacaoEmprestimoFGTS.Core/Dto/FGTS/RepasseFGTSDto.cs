using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Dto.FGTS
{
    public class RepasseFGTSDto
    {
        public decimal ValorParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Aliquota { get; set; }
        public decimal ParcelaAdicional { get; set; }
    }
}
