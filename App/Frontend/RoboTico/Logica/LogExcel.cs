using Newtonsoft.Json;
using RoboTico.Entidades;
using RoboTico.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RoboTico.Logica
{
    public class LogExcel
    {
        private readonly string urlBackend = "https://localhost:44342/api/";

        public async Task<ResGenerarExcel> Generar(ReqGenerarExcel req)
        {
            ResGenerarExcel res = new ResGenerarExcel();
            res.ListaAciertos = new List<string>();
            res.ListaErrores = new List<string>();
            Helpers helper = new Helpers();

            try
            {
                #region Validaciones
                if (!helper.ValidarVacio(req.Contrasena))
                {
                    res.ListaErrores.Add("La contraseña es obligatoria");
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
                            respuestaHttp = await httpClient.PostAsync(urlBackend + "excel/generar", jsonContent);
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
                        res = JsonConvert.DeserializeObject<ResGenerarExcel>(responseContent);
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