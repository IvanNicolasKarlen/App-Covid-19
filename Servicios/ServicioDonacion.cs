using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entidades;
using Entidades.Views;
using DAO;

namespace Servicios
{
    public class ServicioDonacion
    {

        DonacionMonetariaDao DonacionMonetariaDao = new DonacionMonetariaDao();

        public bool MontoADonarRecibido(VMDonacionMonetaria vmdonacionMonetaria)
        {
            if (vmdonacionMonetaria.Dinero < 100)
            {
                return false;
            }
            return true;
        }

        public decimal TotalRecaudado(int IdDonacionMonetaria)
        {
            List<DonacionesMonetarias> lista = DonacionMonetariaDao.ObtenerPorId(IdDonacionMonetaria);

            decimal sumatoria = lista.Sum(item => item.Dinero);
            return sumatoria;
        }


        public NecesidadesDonacionesMonetarias CantidadSolicitada(int IdNecesidadDonacionMonetaria)
        {
            return DonacionMonetariaDao.CantidadSolicitada(IdNecesidadDonacionMonetaria);
        }

        public decimal CalculoRestaDonacion(decimal Suma, decimal CantSolicitada)
        {
            decimal calculo = CantSolicitada - Suma;
            return calculo;
        }
    }
}