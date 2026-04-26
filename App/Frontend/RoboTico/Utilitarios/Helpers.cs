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
using RoboTico.Entidades;
using System.IO;
using System.Data;

namespace RoboTico.Utilitarios
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
    }
}
