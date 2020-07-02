using DAO;
using DAO.Context;
using Entidades;
using Entidades.Views;
using System;
using System.Collections.Generic;

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

        public DonacionesInsumos GuardarCantidadDonada(DonacionesInsumos donacionesI, int idUsuario)
        {
            donacionesI.IdUsuario = idUsuario;
            donacionesI.FechaCreacion = DateTime.Now;

            return DonacionInsumosDao.Guardar(donacionesI);
        }
    }
}
