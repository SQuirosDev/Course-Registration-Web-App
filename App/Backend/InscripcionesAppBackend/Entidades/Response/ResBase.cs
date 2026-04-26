using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesAppBackend.Entidades
{
    public class ResBase
    {
        public bool Resultado { get; set; }
        public List<string> ListaAciertos { get; set; }
        public List<string> ListaErrores { get; set; }
        public int RegistroId { get; set; }
        public int ErrorId { get; set; }
    }
}
