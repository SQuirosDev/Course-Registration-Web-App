using RoboTico.Entidades;
using RoboTico.Logica;
using RoboTico.Modelos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RoboTico.Controllers
{
    public class HomeController : Controller
    {
        // ================================================================
        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // ================================================================
        [System.Web.Mvc.HttpGet]
        public ActionResult Info()
        {
            return View();
        }

        // ================================================================
        [System.Web.Mvc.HttpGet]
        public ActionResult Inscripciones()
        {
            InscripcionesVM inscripcionVM = new InscripcionesVM();
            inscripcionVM.ListaAciertos = new List<string>();
            inscripcionVM.ListaErrores = new List<string>();

            return View(inscripcionVM);
        }

        // POST: Admin/GenerarExcel
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Inscripciones(InscripcionesVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ResIngresarInscripcion res = new ResIngresarInscripcion();
            res.ListaAciertos = new List<string>();
            res.ListaErrores = new List<string>();

            try
            {
                ReqIngresarInscripcion req1 = new ReqIngresarInscripcion
                {
                    Inscripcion = new Inscripciones
                    {
                        Nombre = model.Nombre,
                        Apellido1 = model.Apellido1,
                        Apellido2 = model.Apellido2,
                        Grado = model.Grado,
                        Seccion = model.Seccion,
                        Correo = model.Correo,
                        Confirmado = model.Confirmado,
                    }
                };

                //Llamar logica
                res = await new LogInscripciones().Ingresar(req1);

                if (res.Resultado)
                {
                    model.ListaAciertos = res.ListaAciertos;
                }
                else
                {
                    model.ListaErrores = res.ListaErrores;
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
            return View("Inscripciones", model);
        }
        
        // ================================================================
        [System.Web.Mvc.HttpGet]
        public ActionResult Admin()
        {
            AdminVM adminVM = new AdminVM();
            adminVM.ListaAciertos = new List<string>();
            adminVM.ListaErrores = new List<string>();

            // Al entrar por primera vez, devolvemos un modelo vacío
            return View(adminVM);
        }

        // POST: Admin/GenerarExcel
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Admin(AdminVM model)
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
                    model.ListaAciertos = res.ListaAciertos;
                }
                else
                {
                    model.ListaErrores = res.ListaErrores;
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
            return View("Admin", model);
        }

    }
}