using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesAppBackend.Entidades
{
    public class Inscripciones
    {
        public int InscripcionId { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Grado { get; set; }
        public string Seccion { get; set; }
        public string Correo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Confirmado { get; set; }
    }
}
