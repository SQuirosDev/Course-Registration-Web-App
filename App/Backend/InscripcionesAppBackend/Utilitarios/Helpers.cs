using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClosedXML.Excel;
using InscripcionesAppBackend.Entidades;
using System.IO;
using System.Data;

namespace InscripcionesAppBackend.Utilitarios
{
    public class Helpers
    {
        public bool ValidarVacio(string dato)
        {
            if (string.IsNullOrEmpty(dato) || string.IsNullOrWhiteSpace(dato))
            {
                return false;
            }
            return true;
        }

        public bool ValidarCorreoFormato(string correo)
        {
            // Expresión regular básica pero robusta
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(correo, patron, RegexOptions.IgnoreCase);
        }

        public bool ValidarVerdadero(bool confirmado)
        {
            if (confirmado)
            {
                return true;
            }
            return false;
        }

        public bool EnviarCorreo(string destinatario)
        {
            // Configuración del remitente
            string correoRemitente = "sebastiandev006@gmail.com"; // Cambia esto a tu correo
            string contraseñaApp = "doil fhfk mhlp pmvb"; // Usa una contraseña de aplicación de Gmail

            // Crear el mensaje de correo
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(correoRemitente),
                Subject = "Confirmación de inscripción al curso de Robótica LEGO EV3",
                IsBodyHtml = true, // Habilitar HTML
                Body = GenerarHTMLCorreo() // Método que genera el contenido HTML
            };

            mailMessage.Headers.Add("X-Priority", "1"); // Alta prioridad
            mailMessage.Headers.Add("X-MSMail-Priority", "High");
            mailMessage.Headers.Add("Importance", "High");

            // Agregar destinatario
            mailMessage.To.Add(destinatario);

            try
            {
                // Configuración del servidor SMTP de Gmail
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(correoRemitente, contraseñaApp);
                    smtpClient.EnableSsl = true;

                    // Enviar correo
                    smtpClient.Send(mailMessage);
                }
            }
            catch (SmtpException exMail)
            {
                return false;
            }

            return true;
        }

        private string GenerarHTMLCorreo()
        {
            return @"
                <html>
                    <head>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                padding: 20px;
                            }
                            .container {
                                max-width: 600px;
                                background: #ffffff;
                                padding: 30px;
                                border-radius: 12px;
                                box-shadow: 0 0 12px rgba(0, 0, 0, 0.1);
                                text-align: center;
                            }
                            h1 {
                                color: #2c3e50;
                            }
                            p {
                                font-size: 16px;
                                color: #555;
                                line-height: 1.5;
                            }
                            .icon {
                                font-size: 40px;
                                margin: 15px 0;
                            }
                            .footer {
                                font-size: 12px;
                                color: #888;
                                margin-top: 25px;
                            }
                            img {
                                max-width: 120px;
                                margin-bottom: 20px;
                            }
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <img src='https://img.icons8.com/ios-filled/100/robot-2.png' alt='Robot Icon'/>
                            <h1>¡Inscripción Confirmada!</h1>
                            <p>Hola,</p>
                            <p>Tu inscripción al <b>Curso de Robótica</b> ha sido confirmada con éxito.</p>
                            <p>Estamos muy emocionados de que formes parte de esta experiencia donde aprenderás sobre:</p>
                            <ul style='text-align:left; display:inline-block;'>
                                <li>🧩 Robótica con LEGO EV3: desde lo básico hasta lo avanzado</li>
                                <li>⚙️ Construcción y programación de robots inteligentes</li>
                                <li>🌍 Preparación y desarrollo de proyectos para competencias WRO</li>
                            </ul>
                            <p>Muy pronto recibirás más información con los detalles del curso.</p>
                            <div class='footer'>Este es un mensaje automático. Por favor, no respondas a este correo.</div>
                        </div>
                    </body>
                </html>"
            ;
        }

        public byte[] GenerarExcelInscripciones(List<Inscripciones> listaInscripciones)
        {
            DataTable dt = new DataTable("Inscripciones");
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Apellido 1", typeof(string));
            dt.Columns.Add("Apellido 2", typeof(string));
            dt.Columns.Add("Grado", typeof(string));
            dt.Columns.Add("Sección", typeof(string));
            dt.Columns.Add("Correo", typeof(string));
            dt.Columns.Add("Fecha Registro", typeof(DateTime));
            dt.Columns.Add("Confirmado", typeof(bool));

            foreach (Inscripciones inscripcion in listaInscripciones)
            {
                dt.Rows.Add(inscripcion.InscripcionId, 
                            inscripcion.Nombre, 
                            inscripcion.Apellido1, 
                            inscripcion.Apellido2, 
                            inscripcion.Grado, 
                            inscripcion.Seccion, 
                            inscripcion.Correo, 
                            inscripcion.FechaRegistro, 
                            inscripcion.Confirmado
                );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                IXLWorksheet ws = wb.Worksheets.Add(dt, "Inscripciones");
                ws.Columns().AdjustToContents();

                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return ms.ToArray();
                }
            }
        }

        public bool EnviarCorreoExcel(List<CorreosDestino> listaCorreosDestino, byte[] archivoExcel)
        {
            // Configuración del remitente
            string correoRemitente = "sebastiandev006@gmail.com"; // Cambia esto a tu correo
            string contraseñaApp = "doil fhfk mhlp pmvb"; // Usa una contraseña de aplicación de Gmail

            // Crear el mensaje de correo
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(correoRemitente),
                Subject = "Listado de Inscripciones al Curso de Robótica",
                IsBodyHtml = true, // Habilitar HTML
                Body = GenerarHTMLCorreo2() // Método que genera el contenido HTML
            };

            mailMessage.Headers.Add("X-Priority", "1"); // Alta prioridad
            mailMessage.Headers.Add("X-MSMail-Priority", "High");
            mailMessage.Headers.Add("Importance", "High");

            // Agregar destinatario
            foreach (CorreosDestino correoDestino in listaCorreosDestino)
            {
                mailMessage.To.Add(correoDestino.Correo);
            }

            // Adjuntar Excel
            mailMessage.Attachments.Add(new Attachment(new MemoryStream(archivoExcel), "Inscripciones.xlsx"));

            try
            {
                // Configuración del servidor SMTP de Gmail
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(correoRemitente, contraseñaApp);
                    smtpClient.EnableSsl = true;

                    // Enviar correo
                    smtpClient.Send(mailMessage);
                }
            }
            catch (SmtpException exMail)
            {
                return false;
            }

            return true;
        }

        private string GenerarHTMLCorreo2()
        {
            return @"
                <html>
                  <head>
                    <style>
                      body { font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px; }
                      .container { max-width: 600px; background: #fff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0,0,0,0.1); text-align: center; }
                      h1 { color: #333; }
                      p { color: #555; }
                      .footer { font-size: 12px; color: #666; margin-top: 20px; }
                    </style>
                  </head>
                  <body>
                    <div class=""container"">
                      <h1>Inscripciones al Curso de Robótica</h1>
                      <p>Adjunto encontrarás el archivo Excel con el listado completo de inscripciones.</p>
                      <p>Por favor revisa la información y conserva el documento para tus registros.</p>
                      <div class=""footer"">Este es un mensaje automático. No respondas a este correo.</div>
                    </div>
                  </body>
                </html>"
            ;
        }
    }
}
