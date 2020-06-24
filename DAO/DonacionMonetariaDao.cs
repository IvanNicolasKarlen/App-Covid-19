using DAO.Abstract;
using DAO.Context;
using Entidades;
using Entidades.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAO
{
    public class DonacionMonetariaDao : Crud<DonacionesMonetarias> //Uso de Generics
    {
        public DonacionMonetariaDao(TpDBContext context) : base(context)
        {

        }

        public override DonacionesMonetarias Actualizar(DonacionesMonetarias generics)
        {
            throw new NotImplementedException();
        }

        public override DonacionesMonetarias Crear(DonacionesMonetarias Dinero)
        {
            DonacionesMonetarias donacionesMonetarias = context.DonacionesMonetarias.Add(Dinero);
            decimal valor = context.SaveChanges();

            if (valor >= 100m)
            {
                return donacionesMonetarias;
            }
            else
            {
                return null;
            }
        }

        public DonacionesMonetarias Guardar(DonacionesMonetarias Dinero)
        {
            DonacionesMonetarias donacionesMonetarias = context.DonacionesMonetarias.Add(Dinero);
            context.SaveChanges();
            return donacionesMonetarias;
        }

        public override DonacionesMonetarias ObtenerPorID(int generics)
        {
            throw new NotImplementedException();
        }

        public List<DonacionesMonetarias> ObtenerPorId(int IdNeceDonacionMonetaria)
        {
            List<DonacionesMonetarias> listarCantidadNecesariaADonar =
            context.DonacionesMonetarias.Where(a => a.IdNecesidadDonacionMonetaria == IdNeceDonacionMonetaria).ToList();
            return listarCantidadNecesariaADonar;
        }



        public DonacionesMonetarias ObtenerDonacionMonetariaPorId(int IdDonacionMonetaria)
        {
            DonacionesMonetarias donacioMnPorId = context.DonacionesMonetarias.Find(IdDonacionMonetaria);
            return donacioMnPorId;
        }

        public DonacionesMonetarias ActualizarComprobante(VMComprobantePago donaM)
        {
            DonacionesMonetarias DonacionesMonetariasBd = ObtenerDonacionMonetariaPorId(donaM.IdDonacionMonetaria);
            DonacionesMonetariasBd.ArchivoTransferencia = donaM.ArchivoTransferencia;
            context.SaveChanges();
            return DonacionesMonetariasBd;
        }
    }
}
