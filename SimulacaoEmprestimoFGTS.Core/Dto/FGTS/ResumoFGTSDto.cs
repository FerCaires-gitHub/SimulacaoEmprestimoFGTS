using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Dto.FGTS
{
    public class ResumoFGTSDto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public double TaxaMensal { get; set; }
        public decimal Solicitado { get; set; }
        public decimal Juros { get; set; }
        public decimal IOF { get; set; }
        public decimal ValorLiberado { get; set; }
        public decimal ValorOperacao { get; set; }
    }
}
