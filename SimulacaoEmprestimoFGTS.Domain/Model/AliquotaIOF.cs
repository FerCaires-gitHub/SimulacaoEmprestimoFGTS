using System;
using System.Collections.Generic;
using System.Text;

namespace SimulacaoEmprestimoFGTS.Domain.Model
{
    public class AliquotaIOF
    {
        public double Mensal { get; set; }
        public double Anual { get; set; }

        public static AliquotaIOF GetAliquotaIOF(DateTime dateTime)
        {
            if(dateTime <= Convert.ToDateTime("2021-09-14")) 
                return new AliquotaIOF { Mensal = 0.000082, Anual = 0.030 };
            return new AliquotaIOF { Mensal = 0.0001118, Anual = 0.0408 };
        }
    }
}
