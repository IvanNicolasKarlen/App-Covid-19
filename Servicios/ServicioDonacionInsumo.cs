using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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

        public DonacionesInsumos GuardarCantidadDonada(VMNecesidadesDonacionesInsumos vmNDI, int idUsuario)
        {
            DonacionesInsumos donacionI = new DonacionesInsumos()
            {
                Cantidad = vmNDI.Cantidad,
                IdUsuario = idUsuario,
                IdNecesidadDonacionInsumo = vmNDI.IdNecesidadDonacionInsumo,
                FechaCreacion = DateTime.Now
            };
            return DonacionInsumosDao.GuardarInsumo(donacionI);
        }

        public NecesidadesDonacionesInsumos ObtenerNecesidadDonacionInsumosPorId(int idNecesidadDonacionInsumo)
        {
            return DonacionInsumosDao.ObtenerNecesidadDonacionInsumosPorId(idNecesidadDonacionInsumo);

        }

        public NecesidadesDonacionesInsumos ObtenerCantidadPorId(VMNecesidadesDonacionesInsumos idNecesidadDonacionInsumo)
        {
            return DonacionInsumosDao.ObtenerNecesidadDonacionInsumosPorId(idNecesidadDonacionInsumo.IdNecesidadDonacionInsumo);

        }
    }
}
