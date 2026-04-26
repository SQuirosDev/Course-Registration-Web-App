using RoboTico.Entidades;
using Newtonsoft.Json;
using RoboTico.Utilitarios;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace RoboTico.Logica
{
    public class LogInscripciones
    {
        private readonly string urlBackend = "https://localhost:44342/api/";

        public async Task<ResIngresarInscripcion> Ingresar(ReqIngresarInscripcion req)
        {
            ResIngresarInscripcion res = new ResIngresarInscripcion ();
            res.ListaAciertos = new List<string>();
            res.ListaErrores = new List<string>();
            Helpers helper = new Helpers();

            try
            {
                #region Validaciones
                // Validacion de vacio
                if (!helper.ValidarVacio(req.Inscripcion.Nombre))
                {
                    res.ListaErrores.Add("El nombre es obligatorio");
                }
                if (!helper.ValidarVacio(req.Inscripcion.Apellido1))
                {
                    res.ListaErrores.Add("El primer apellido es obligatorio");
                }
                if (!helper.ValidarVacio(req.Inscripcion.Apellido2))
                {
                    res.ListaErrores.Add("El segundo apellido es obligatorio");
                }
                if (!helper.ValidarVacio(req.Inscripcion.Grado))
                {
                    res.ListaErrores.Add("El grado es obligatorio");
                }
                if (!helper.ValidarVacio(req.Inscripcion.Seccion))
                {
                    res.ListaErrores.Add("La seccion es obligatoria");
                }
                if (!helper.ValidarVacio(req.Inscripcion.Correo))
                {
                    res.ListaErrores.Add("El correo es obligatorio");
                }
                if (!helper.ValidarVerdadero(req.Inscripcion.Confirmado))
                {
                    res.ListaErrores.Add("La confirmacion es obligatoria");
                }

                // Validacion de formato
                if (!helper.ValidarCorreoFormato(req.Inscripcion.Correo))
                {
                    res.ListaErrores.Add("El formato del correo no es el correcto");
                }
                #endregion

                if (res.ListaErrores.Any())
                {
                    res.Resultado = false;
                    res.ListaAciertos.Add("");
                    res.RegistroId = 0;
                    res.ErrorId = 1;
                }
                else
                {
                    // Objeto json
                    StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                    //
                    HttpResponseMessage respuestaHttp = new HttpResponseMessage();

                    //Consulta a la API
                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            respuestaHttp = await httpClient.PostAsync(urlBackend + "inscripciones/ingresar", jsonContent);
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        res.Resultado = false;
                        res.ListaAciertos.Add("");
                        res.ListaErrores.Add("Error al conectar con el servidor");
                        res.RegistroId = 0;
                        res.ErrorId = 1;
                    }

                    if (respuestaHttp.Content == null)
                    {
                        return res;
                    }
                    else
                    {
                        //Convierte el JSON a objeto
                        string responseContent = await respuestaHttp.Content.ReadAsStringAsync();
                        res = JsonConvert.DeserializeObject<ResIngresarInscripcion>(responseContent);
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaAciertos.Add("");
                res.ListaErrores.Add("Ha habido un error con la solicitud, intente mas tarde");
                res.RegistroId = 0;
                res.ErrorId = 1;
            }

            return res;
        }
    }
}
