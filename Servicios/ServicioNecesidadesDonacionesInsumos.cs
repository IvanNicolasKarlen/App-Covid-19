using DAO;
using DAO.Context;
using Entidades;
using Entidades.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ServicioNecesidadesDonacionesInsumos
    {
        DonacionInsumosDao DonacionInsumosDao;
        NecesidadesDonacionesInsumosDAO necesidadesDonacionesInsumosDAO;
        public ServicioNecesidadesDonacionesInsumos(TpDBContext context)
        {
            DonacionInsumosDao = new DonacionInsumosDao(context);
            necesidadesDonacionesInsumosDAO = new NecesidadesDonacionesInsumosDAO(context);
        }

        public List<NecesidadesDonacionesInsumos> ListaNombre(NecesidadesDonacionesInsumos idNecesidad)
        {
            return necesidadesDonacionesInsumosDAO.BuscarPorId(idNecesidad.IdNecesidad);
        }


        public NecesidadesDonacionesInsumos ObtenerNecesidadDonacionInsumosPorId(int idNecesidadDonacionInsumo)
        {
            return necesidadesDonacionesInsumosDAO.ObtenerNecesidadDonacionInsumosPorId(idNecesidadDonacionInsumo);
        }

        /*public NecesidadesDonacionesInsumos ObtenerCantidadPorId(VMNecesidadesDonacionesInsumos idNecesidadDonacionInsumo)
        {
            return DonacionInsumosDao.ObtenerNecesidadDonacionInsumosPorId(idNecesidadDonacionInsumo.IdNecesidadDonacionInsumo);
        }*/
    }
}
