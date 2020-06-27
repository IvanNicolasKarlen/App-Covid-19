using DAO.Abstract;
using DAO.Context;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAO
{
    public class DonacionInsumosDao : Crud<DonacionesInsumos>
    {
        public DonacionInsumosDao(TpDBContext context) : base(context)
        {
        }

        public override DonacionesInsumos Crear(DonacionesInsumos generics)
        {
            throw new NotImplementedException();
        }

        public override DonacionesInsumos Actualizar(DonacionesInsumos generics)
        {
            throw new NotImplementedException();
        }

        public override DonacionesInsumos ObtenerPorID(int id)
        {
            throw new NotImplementedException();
        }

        public DonacionesInsumos GuardarInsumo(DonacionesInsumos donacionInsumo)
        {
            DonacionesInsumos donacion = context.DonacionesInsumos.Add(donacionInsumo);
            context.SaveChanges();
            return donacion;
        }
    }
}
