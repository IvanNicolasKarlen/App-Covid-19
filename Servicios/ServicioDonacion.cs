using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entidades;
using Entidades.Views;
using DAO;
using DAO.Context;

namespace Servicios
{
    public class ServicioDonacion
    {

        DonacionMonetariaDao DonacionMonetariaDao ;

        public ServicioDonacion(TpDBContext context)
        {
            DonacionMonetariaDao = new DonacionMonetariaDao(context);
        }

       
        public decimal TotalRecaudado(int IdNeceDonacionMonetaria)
        {
            List<DonacionesMonetarias> lista = DonacionMonetariaDao.ObtenerPorId(IdNeceDonacionMonetaria);

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

        public DonacionesMonetarias GuardarDonacionM(VMDonacionMonetaria vmDonacionMonetaria, int idUsuario )
        {
            DonacionesMonetarias donacionM = new DonacionesMonetarias()
            {
                Dinero = vmDonacionMonetaria.Dinero,
                IdNecesidadDonacionMonetaria = vmDonacionMonetaria.IdNecesidadDonacionMonetaria,
                IdUsuario = idUsuario,
                FechaCreacion = DateTime.Now,
                ArchivoTransferencia = ""
                };

            return DonacionMonetariaDao.Guardar(donacionM);
        }

        //ACA SE GUARDA EL NOMBRE DEL COMPROBANTE DE PAGO EN LA BD.
        public DonacionesMonetarias Actualizar(VMComprobantePago idDonaM)
        {
            string ArchivoTransferencia = idDonaM.ArchivoTransferencia;
            int id = idDonaM.IdDonacionMonetaria;
            return DonacionMonetariaDao.ActualizarComprobante(ArchivoTransferencia,id);
        }
    }
}