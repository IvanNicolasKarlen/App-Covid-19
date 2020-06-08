using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCovid19.Utilities
{
    public class LikeOrDislike
    {
        public bool AgregaLikeOrDislike(int idSession,string boton, int idNecesidad, ServicioNecesidadValoraciones servicioValoraciones)
        {
            bool likeOrDislike = servicioValoraciones.guardarValoracion(idSession, idNecesidad, boton);
            return likeOrDislike;
        }
    }
}