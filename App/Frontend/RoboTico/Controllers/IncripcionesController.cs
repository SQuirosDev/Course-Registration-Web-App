using System.Threading.Tasks;
using System.Web.Mvc;
using RoboTico.Logica;
using RoboTico.Entidades;
using System.Collections.Generic;

namespace RoboTico.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly LogInscripciones logica = new LogInscripciones();

        // GET: Inscripciones
        public ActionResult Index()
        {
            return View();
        }

        // GET: Inscripciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inscripciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ReqIngresarInscripcion req)
        {
            if (!ModelState.IsValid)
            {
                return View(req);
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
                        Nombre = "",
                        Apellido1 = "",
                        Apellido2 = "",
                        Grado = "",
                        Seccion = "",
                        Correo = "",
                        Confirmado = true
                    }
                };

                //Llamar logica
                res = await new LogInscripciones().Ingresar(req1);

                if (res.Resultado)
                {
                    // Tirar alerta con Aciertos
                }
                else
                {
                    // Tirar alerta con errores
                }

                // Limpiar los TXT
            }
            catch
            {
                // Alerta
            }

            return View(req);

            /*
            ResBase res = await logica.Ingresar(req);

            if (res.Resultado)
            {
                TempData["Mensaje"] = "Inscripción realizada correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                // Mostrar errores en la vista
                foreach (var error in res.ListaErrores)
                {
                    ModelState.AddModelError("", error);
                }
                return View(req);
            }
            */
        }
    }
}
