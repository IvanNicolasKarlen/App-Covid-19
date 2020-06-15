using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DAO.Context;
using Entidades;

namespace Servicios
{
    public class ServicioDonacionInsumo
    {
        DonacionInsumosDao DonacionInsumosDao;
        public ServicioDonacionInsumo(TpDBContext context)
        {
            DonacionInsumosDao = new DonacionInsumosDao(context);

        }


        public bool CantidadMinimaDeInsumo(DonacionesInsumos DonacionesInsumos)
        {

            if (DonacionesInsumos.Cantidad < 1)
            {
                return false;
            }
            return true;
        }

        public List<NecesidadesDonacionesInsumos> ListaNombre(NecesidadesDonacionesInsumos idNecesidad)
        {
            return DonacionInsumosDao.BuscarPorId(idNecesidad.IdNecesidad);
        }
    }
}
