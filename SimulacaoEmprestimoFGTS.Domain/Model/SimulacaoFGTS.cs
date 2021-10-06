using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Domain.Model
{
    public class SimulacaoFGTS
    {
        public RepasseFGTS Repasse { get; set; }
        public decimal Principal { get; set; }
        public decimal Juros { get; set; }
        public decimal IOF { get; set; }
        public decimal ValorLiberado { get; set; }
    }
}
