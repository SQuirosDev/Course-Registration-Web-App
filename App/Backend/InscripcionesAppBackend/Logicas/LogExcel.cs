using InscripcionesAppBackend.Entidades;
using InscripcionesAppBackend.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesAppBackend.Logicas
{
    public class LogExcel
    {
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
                    if (req.Contrasena == "7ce04616-9b72-4ee6-bf7e-58fcaac087a9")
                    {
                        List<Inscripciones> listaInscripciones = new List<Inscripciones>();
                        List<CorreosDestino> listaCorreosDestino = new List<CorreosDestino>();

                        listaInscripciones.AddRange(new LogInscripciones().Listar().ListaInscripciones);
                        listaCorreosDestino.AddRange(new LogCorreosDestino().Listar().ListaCorreosDestino);

                        if (helper.EnviarCorreoExcel(listaCorreosDestino, helper.GenerarExcelInscripciones(listaInscripciones)))
                        {
                            res.Resultado = true;
                            res.ListaAciertos.Add("Se enviaron los correos a sus destinos correctamente");
                            res.ListaErrores.Add("");
                            res.RegistroId = 1;
                            res.ErrorId = 0;
                        }
                    }
                    else
                    {
                        res.Resultado = false;
                        res.ListaAciertos.Add("");
                        res.ListaErrores.Add("La contraseña no es la correcta");
                        res.RegistroId = 0;
                        res.ErrorId = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                res.Resultado = false;
                res.ListaAciertos.Add("");
                res.ListaErrores.Add("Hubo un problema al generar el excel, intente más tarde");
                res.RegistroId = 0;
                res.ErrorId = 1;
            }

            return res;
        } 
    }
}