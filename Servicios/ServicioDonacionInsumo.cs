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
                IdNecesidadDonacionInsumo = vmNDI.IdNecesidadDonacionInsumo
            };
            return DonacionInsumosDao.GuardarInsumo(donacionI);
        }

        public int TraerCantidadDonada(int idNecesidadDonacionInsumo)
        {
            List<DonacionesInsumos> donacionesIn = DonacionInsumosDao.CantidadDonada(idNecesidadDonacionInsumo);
            int suma = donacionesIn.Sum(item => item.Cantidad);
            return suma;
        }

        public NecesidadesDonacionesInsumos ObtenerCantidadPorId(VMNecesidadesDonacionesInsumos idNecesidadDonacionInsumo)
        {
           return DonacionInsumosDao.BuscarCantidadDeInsumosPorId(idNecesidadDonacionInsumo.IdNecesidadDonacionInsumo);
        
        }

        public int InsumoRestante(int cantidadInsumosSolicitado, int resultado)
        {
            int restante = cantidadInsumosSolicitado - resultado;
            return restante;
        }
    }
}
