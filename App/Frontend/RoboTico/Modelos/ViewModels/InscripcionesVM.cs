using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoboTico.Modelos.ViewModels
{
    public class InscripcionesVM : ResBaseVM
    {
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Grado { get; set; }
        public string Seccion { get; set; }
        public string Correo { get; set; }
        public bool Confirmado { get; set; }
    }
}