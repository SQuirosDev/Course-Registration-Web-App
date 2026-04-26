using InscripcionesAppBackend.Entidades;
using InscripcionesAppBackend.Logicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace InscripcionesAppRestAPI.Controllers
{
    public class InscripcionesController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/inscripciones/ingresar")]
        public async Task<ResIngresarInscripcion> Ingresar (ReqIngresarInscripcion req)
        {
            return await new LogInscripciones().Ingresar(req);
        }
    }
}