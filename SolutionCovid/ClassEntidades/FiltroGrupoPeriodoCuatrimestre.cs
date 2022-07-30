using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassEntidades
{
    public class FiltroGrupoPeriodoCuatrimestre
    {
        public int id_GruCuat { get; set; }
        public string Grupo { get; set; }
        public string Turno { get; set; }
        public string Modalidad { get; set; }
        public string Periodo { get; set; }
        public int Anio { get; set; }
    }
}
