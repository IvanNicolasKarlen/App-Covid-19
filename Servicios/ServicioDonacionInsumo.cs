using Entidades.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCovid19.Services
{
    public class ServicioDonacionInsumo
    {
        public bool CantidadMinimaDeInsumo(DonacionesInsumos DonacionesInsumos)
        {

            if (DonacionesInsumos.Cantidad < 1)
            {
                return false;
            }
            return true;
        }
    }
}
