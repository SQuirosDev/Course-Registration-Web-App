using InscripcionesAppAccesoDatos.AccesoDatos;
using InscripcionesAppBackend.Entidades;
using InscripcionesAppBackend.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesAppBackend.Logicas
{
    public class LogInscripciones
    {
        public async Task<ResIngresarInscripcion> Ingresar (ReqIngresarInscripcion req)
        {
            ResIngresarInscripcion res = new ResIngresarInscripcion ();
            res.ListaAciertos = new List<string> ();
            res.ListaErrores = new List<string> ();
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
                    Inscripciones inscripcion = new Inscripciones();

                    // Outputs BD
                    bool? Resultado = false;
                    string ListaAciertos = "";
                    string ListaErrores = "";
                    int? RegistroId = 0;
                    int? ErrorId = 0;

                    try
                    {
                        using (ConexionDataContext LinQ = new ConexionDataContext())
                        {
                            LinQ.PS_INSERT_INSCRIPCION(req.Inscripcion.Nombre,
                                                        req.Inscripcion.Apellido1,
                                                        req.Inscripcion.Apellido2,
                                                        req.Inscripcion.Grado,
                                                        req.Inscripcion.Seccion,
                                                        req.Inscripcion.Correo,
                                                        req.Inscripcion.Confirmado,
                                                        ref Resultado,
                                                        ref ListaAciertos,
                                                        ref ListaErrores,
                                                        ref RegistroId,
                                                        ref ErrorId
                            );
                        }
                    }
                    catch (SqlException exSQL)
                    {
                        res.Resultado = false;
                        res.ListaAciertos.Add("");
                        res.ListaErrores.Add("Hubo un problema la guardar la informacion, intente más tarde");
                        res.RegistroId = 0;
                        res.ErrorId = 2;
                    }

                    if (RegistroId >= 1)
                    {
                        res.Resultado = (bool)Resultado;
                        res.ListaAciertos.Add(ListaAciertos);
                        res.ListaErrores.Add(ListaErrores);
                        res.RegistroId = (int)RegistroId;
                        res.ErrorId = (int)ErrorId;

                        if (helper.EnviarCorreo(req.Inscripcion.Correo))
                        {
                            res.ListaAciertos.Add("El correo fue enviado correctamente, Si no le aparece en la bandeja de entrada revise el spam o el correo no deseado");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaAciertos.Add("");
                res.ListaErrores.Add("Hubo un problema al guardar la informacion, intente más tarde");
                res.RegistroId = 0;
                res.ErrorId = 1;
            }

            return res;
        }

        public ResListarInscripciones Listar ()
        {
            ResListarInscripciones res = new ResListarInscripciones();
            res.ListaAciertos = new List<string>();
            res.ListaErrores = new List<string>();
            res.ListaInscripciones = new List<Inscripciones>();

            try
            {
                List<PS_GET_INSCRIPCIONESResult> listaInscripcionesTC = new List<PS_GET_INSCRIPCIONESResult> ();

                // Outputs BD
                bool? Resultado = false;
                string ListaAciertos = "";
                string ListaErrores = "";
                int? RegistroId = 0;
                int? ErrorId = 0;

                try
                {
                    using (ConexionDataContext LinQ = new ConexionDataContext())
                    {
                        listaInscripcionesTC = LinQ.PS_GET_INSCRIPCIONES(ref Resultado,
                                                                            ref ListaAciertos,
                                                                            ref ListaErrores,
                                                                            ref RegistroId,
                                                                            ref ErrorId
                        ).ToList();
                    }
                }
                catch (SqlException exSQL)
                {
                    res.Resultado = false;
                    res.ListaAciertos.Add("");
                    res.ListaErrores.Add("Hubo un problema la guardar la informacion, intente más tarde");
                    res.RegistroId = 0;
                    res.ErrorId = 2;
                }

                if (RegistroId >= 1)
                {                    
                    foreach (PS_GET_INSCRIPCIONESResult inscripcionTC in listaInscripcionesTC)
                    {
                        res.ListaInscripciones.Add(new Factorias().FactoriaInscripciones(inscripcionTC));
                    }

                    res.Resultado = (bool)Resultado;
                    res.ListaAciertos.Add(ListaAciertos);
                    res.ListaErrores.Add(ListaErrores);
                    res.RegistroId = (int)RegistroId;
                    res.ErrorId = (int)ErrorId;
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaAciertos.Add("");
                res.ListaErrores.Add("Hubo un problema la guardar la informacion, intente más tarde");
                res.RegistroId = 0;
                res.ErrorId = 1;
            }

            return res;
        }
    }
}
