using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoboTico.Modelos.ViewModels
{
    public class ResBaseVM
    {
        public List<string> ListaAciertos { get; set; }
        public List<string> ListaErrores { get; set; }
    }
}