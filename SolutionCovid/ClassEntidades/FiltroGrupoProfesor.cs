using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassEntidades
{
    public class FiltroGrupoProfesor
    {
        public int ID_ProfeGru { get; set; }
        public string Profesor { get; set; }
        public string Turno{ get; set; }
        public string Modalidad { get; set; }
        public string Grado { get; set; }
        public string Letra { get; set; }
        public int Id_Grupo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string Periodo { get; set; }
        public string Anio { get; set; }
    }
}
