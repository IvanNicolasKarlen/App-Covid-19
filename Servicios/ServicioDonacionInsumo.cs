using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DAO.Context;
using Entidades;
using Entidades.Views;

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

        public NecesidadesDonacionesInsumos BuscarNecesidadesDonacionIPorId(int idNecesidadDonacionInsumo)
        {
            return DonacionInsumosDao.BuscarNecesidadesDonacionIPorId(idNecesidadDonacionInsumo);
        }

        //ToDo: NO harcodear IdNecesidadDonacionInsumo
        public DonacionesInsumos GuardarCantidadDonada(VMNecesidadesDonacionesInsumos vmNDI, int idUsuario)
        {
            DonacionesInsumos donacionI = new DonacionesInsumos()
            {
                Cantidad = vmNDI.Cantidad,
                IdUsuario = idUsuario,
                IdNecesidadDonacionInsumo = 1
            };
            return DonacionInsumosDao.GuardarInsumo(donacionI);
        }
    }
}
