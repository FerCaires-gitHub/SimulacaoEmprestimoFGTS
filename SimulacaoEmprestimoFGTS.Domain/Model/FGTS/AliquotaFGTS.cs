using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Domain.Model.FGTS
{
    public class AliquotaFGTS
    {
        public decimal Minimo { get; set; }
        public decimal Maximo { get; set; }
        public decimal Percentual { get; set; }
        public decimal ParcelaAdicional { get; set; }


        public static IEnumerable<AliquotaFGTS> GetAliquotasFGTS()
        {
            return new List<AliquotaFGTS>()
            {
                new AliquotaFGTS{Minimo = 0, Maximo = 500, ParcelaAdicional =0, Percentual=50 } ,
                new AliquotaFGTS{Minimo = 500.01M, Maximo = 1000, ParcelaAdicional =50, Percentual=40 } ,
                new AliquotaFGTS{Minimo = 1000.01M, Maximo = 5000, ParcelaAdicional =150, Percentual=30 } ,
                new AliquotaFGTS{Minimo = 5000.01M, Maximo = 10000, ParcelaAdicional =650, Percentual=20 } ,
                new AliquotaFGTS{Minimo = 10000.01M, Maximo = 15000, ParcelaAdicional =1150, Percentual=15 } ,
                new AliquotaFGTS{Minimo = 15000.01M, Maximo = 20000, ParcelaAdicional =1900, Percentual=10 } ,
                new AliquotaFGTS{Minimo = 20000.01M, Maximo = 20000000, ParcelaAdicional =2900, Percentual=5 }
            };
        }
    }
}
