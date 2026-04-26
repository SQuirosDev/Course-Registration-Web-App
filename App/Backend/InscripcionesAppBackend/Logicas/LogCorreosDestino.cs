using InscripcionesAppAccesoDatos.AccesoDatos;
using InscripcionesAppBackend.Entidades;
using InscripcionesAppBackend.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesAppBackend.Logicas
{
    public class LogCorreosDestino
    {
        public ResListarCorreosDestino Listar ()
        {
            ResListarCorreosDestino res = new ResListarCorreosDestino ();
            res.ListaAciertos = new List<string> ();
            res.ListaErrores = new List<string> ();
            res.ListaCorreosDestino = new List<CorreosDestino> ();

            try
            {
                List<PS_GET_CORREOSResult> listaCorreosDestinoTC = new List<PS_GET_CORREOSResult>();

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
                        listaCorreosDestinoTC = LinQ.PS_GET_CORREOS(ref Resultado,
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
                    foreach (PS_GET_CORREOSResult correoDestinoTC in listaCorreosDestinoTC)
                    {
                        res.ListaCorreosDestino.Add(new Factorias().FactoriaCorreosDestino(correoDestinoTC));
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