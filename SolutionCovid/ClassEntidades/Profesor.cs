using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassEntidades
{
    public class Profesor
    {
        public int ID_Profe { get; set; }
        public int RegistroEmpleado { get; set; }
        public string Nombre { get; set; }
        public string ap_pat { get; set; }
        public string ap_mat { get; set; }
        public string Genero { get; set; }
        public string Categoria { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public Byte F_EdoCivil { get; set; }
    }
}
