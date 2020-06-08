using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Context;
using Entidades;

namespace Servicios
{
    public class ServicioDonacionInsumo
    {
        public ServicioDonacionInsumo(TpDBContext context)
        {

        }

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
