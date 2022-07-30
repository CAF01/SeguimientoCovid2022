using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassEntidades
{
    public class Cuatrimestre
    {
        public int id { set; get; }
        public string periodo { set; get; }
        public int anio { set; get; }
        public DateTime fechaInicio { set; get; }
        public DateTime fechaFin { set; get; }
        public string extra { set; get; }

        public string datosCuatrimestre()
        {
            return "Periodo: " + this.periodo + " Año: " + this.anio + " Fecha inicio: " +
                this.fechaInicio + " Fecha fin: " + this.fechaFin + " Extra: " + this.extra;
        }
    }
}
