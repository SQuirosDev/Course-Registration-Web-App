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
    public class ExcelController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/excel/generar")]
        public async Task<ResGenerarExcel> Generar(ReqGenerarExcel req)
        {
            return await new LogExcel().Generar(req);
        }
    }
}