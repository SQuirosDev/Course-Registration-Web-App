using InscripcionesAppAccesoDatos.AccesoDatos;
using InscripcionesAppBackend.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscripcionesAppBackend.Utilitarios
{
    public class Factorias
    {
        public Inscripciones FactoriaInscripciones(PS_GET_INSCRIPCIONESResult INSCRIPCION_TC)
        {
            return new Inscripciones
            {
                InscripcionId = INSCRIPCION_TC.INSCRIPCION_ID,
                Nombre = INSCRIPCION_TC.NOMBRE,
                Apellido1 = INSCRIPCION_TC.APELLIDO_1,
                Apellido2 = INSCRIPCION_TC.APELLIDO_2,
                Grado = INSCRIPCION_TC.GRADO,
                Seccion = INSCRIPCION_TC.SECCION,
                Correo = INSCRIPCION_TC.CORREO,
                FechaRegistro = INSCRIPCION_TC.FECHA_REGISTRO,
                Confirmado = INSCRIPCION_TC.CONFIRMADO
            };
        }

        public CorreosDestino FactoriaCorreosDestino (PS_GET_CORREOSResult CORREOS_DESTINO )
        {
            return new CorreosDestino
            {
                CorreoDestinoId = CORREOS_DESTINO.CORREO_ID,
                Correo = CORREOS_DESTINO.CORREO
            };
        }
    }
}
