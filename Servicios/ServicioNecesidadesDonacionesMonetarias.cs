using DAO;
using DAO.Context;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ServicioNecesidadesDonacionesMonetarias
    {
        DonacionMonetariaDao DonacionMonetariaDao;
        NecesidadesDonacionesMonetariasDAO NecesidadesDonacionesMonetariasDAO;

        public ServicioNecesidadesDonacionesMonetarias(TpDBContext context)
        {
            DonacionMonetariaDao = new DonacionMonetariaDao(context);
            NecesidadesDonacionesMonetariasDAO = new NecesidadesDonacionesMonetariasDAO(context);
        }


       /* public NecesidadesDonacionesMonetarias ObtenerIdNecesidadDonacionMonetaria(int IdNecesidadDonacionMonetaria)
        {
            return DonacionMonetariaDao.ObtenerPorIdNecesidadDonacionMonetaria(IdNecesidadDonacionMonetaria);
        }*/

        public NecesidadesDonacionesMonetarias ObtenerPorId(int idNecesidadDonacionMonetaria)
        {
            return NecesidadesDonacionesMonetariasDAO.ObtenerPorID(idNecesidadDonacionMonetaria);
        }
    }
}
