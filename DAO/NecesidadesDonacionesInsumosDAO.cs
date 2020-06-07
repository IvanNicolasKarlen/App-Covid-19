using System;
using System.Collections.Generic;
using System.Linq;
using DAO.Abstract;
using Entidades;

namespace DAO
{
    public class NecesidadesDonacionesInsumosDAO : Crud<NecesidadesDonacionesInsumos> //Uso de Generics
    {
        public override NecesidadesDonacionesInsumos Actualizar(NecesidadesDonacionesInsumos generics)
        {
            NecesidadesDonacionesInsumos insumo = ObtenerPorID(generics.IdNecesidadDonacionInsumo);
            insumo = generics;
            if (context.Entry(insumo).State == System.Data.Entity.EntityState.Modified)
            {
                context.SaveChanges();
                return insumo;
            }
            else
            {
                return null;
            }

        }

        public override NecesidadesDonacionesInsumos Crear(NecesidadesDonacionesInsumos generics)
        {
            NecesidadesDonacionesInsumos insumo = context.NecesidadesDonacionesInsumos.Add(generics);
            context.SaveChanges();
            return insumo;
        }

        public NecesidadesDonacionesInsumos ObtenerPorIDNecesidad(int idNecesidad)
        {
            NecesidadesDonacionesInsumos obtenido= context.NecesidadesDonacionesInsumos.Where(o => o.IdNecesidad == idNecesidad).FirstOrDefault();
            return obtenido;
        }

        public override NecesidadesDonacionesInsumos ObtenerPorID(int id)
        {
            return context.NecesidadesDonacionesInsumos.Find(id);
        }
    }
}
