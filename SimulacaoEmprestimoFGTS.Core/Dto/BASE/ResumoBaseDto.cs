using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Core.Dto.BASE
{
    public class ResumoBaseDto
    {
        public decimal ValorTotal { get; set; }
        public decimal ValorFinanciado { get; set; }
        public decimal Juros { get; set; }
        public int Tempo { get; set; }
    }
}
