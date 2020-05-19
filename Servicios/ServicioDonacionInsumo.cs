using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCovid19;

namespace Servicios
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
