using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Domain.Model.FGTS
{
    public class RepasseFGTS
    {
        public decimal ValorParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public AliquotaFGTS Aliquota { get; set; }
    }
}
