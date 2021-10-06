using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Dto
{
    public class SimulacaoFGTSDto
    {
        public decimal ValorRepasseFGTS { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Principal { get; set; }
        public decimal Juros { get; set; }
        public decimal IOF { get; set; }
        public decimal ValorLiberado { get; set; }
    }
}
