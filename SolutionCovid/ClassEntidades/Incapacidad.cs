using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassEntidades
{
    public class Incapacidad
    {
        public int id_Incapacidad { get; set; }
        public DateTime Fecha_otorga { get; set; }
        public DateTime Fecha_finalizacion { get; set; }
        public string IncapacidadUrl { get; set; }
        public int id_posProfe { get; set; }
    }
}
