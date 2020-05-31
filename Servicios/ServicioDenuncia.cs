using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios
{
    public class ServicioDenuncia
    {
        /// <summary>
        /// Guardar la denuncia y validar si es necesario enviarselo al Admin o no
        /// </summary>
        /// <param name="denuncia"></param>
        /// <returns>True o False</returns>
        public bool guardarDenuncia(Denuncias denuncia)
        {
            List<Denuncias> denunciasObtenidas = new List<Denuncias>();
            //Registro la denuncia
            //Recibo lista de denuncias de esa publicacion
            //Validar si supera a 5 para enviarle la publicacion al admin.

            //Validar si se guardo 



            if (denunciasObtenidas.Count >= 5)
            {
                //Enviarsela al admin
            }


            return true;
        }
    }
}