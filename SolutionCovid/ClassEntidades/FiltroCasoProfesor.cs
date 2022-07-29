using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassEntidades
{
    public class FiltroCasoProfesor
    {
        public int Registro_Positivo { get; set; }
        public int Registro_Profesor { get; set; }
        public string Profesor { get; set; }
        public string Genero { get; set; }
        public DateTime Caso_Confirmado{ get; set; }
        public string Nivel_Riesgo { get; set; }
        public string Antecedentes { get; set; }
        public Byte NumContagio { get; set; }
        public string Extra { get; set; }
    }
}
