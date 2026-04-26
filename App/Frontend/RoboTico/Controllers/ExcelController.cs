using RoboTico.Entidades;
using RoboTico.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RoboTico.Modelos.ViewModels;
using DocumentFormat.OpenXml.Drawing;

namespace RoboTico.Controllers
{
    public class ExcelController : Controller
    {
        // GET: Admin/GenerarExcel
        [System.Web.Mvc.HttpGet]
        public ActionResult GenerarExcel()
        {
            AdminVM adminVM = new AdminVM();
            adminVM.ListaAciertos = new List<string>();
            adminVM.ListaErrores = new List<string>();

            // Al entrar por primera vez, devolvemos un modelo vacío
            return View(adminVM);
        }

        // POST: Admin/GenerarExcel
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> GenerarExcel(AdminVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ResGenerarExcel res = new ResGenerarExcel();
            res.ListaAciertos = new List<string>();
            res.ListaErrores = new List<string>();

            try
            {
                ReqGenerarExcel req1 = new ReqGenerarExcel
                {
                    Contrasena = model.Contrasena
                };

                //Llamar logica
                res = await new LogExcel().Generar(req1);

                if (res.Resultado)
                {
                    model.ListaAciertos.AddRange(res.ListaAciertos);
                }
                else
                {
                    model.ListaErrores.AddRange(res.ListaErrores);
                }

                // Limpiar los TXT
            }
            catch
            {
                if (res.ListaErrores.Any())
                {
                    model.ListaErrores.AddRange(res.ListaErrores);
                }
                else
                {
                    model.ListaErrores.Add("Ha habido un error con la solicitud, intente mas tarde");
                }
            }

            // Retornamos la misma vista con el modelo ya lleno de mensajes
            return View(model);
        }
    }
}