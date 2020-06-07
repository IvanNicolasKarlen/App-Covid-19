using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Abstract;
using Entidades;
namespace DAO
{
    public class NecesidadesDonacionesMonetariasDAO : Crud<NecesidadesDonacionesMonetarias>
    {
        public override NecesidadesDonacionesMonetarias Actualizar(NecesidadesDonacionesMonetarias generics)
        {
            NecesidadesDonacionesMonetarias monetaria = ObtenerPorID(generics.IdNecesidadDonacionMonetaria);
            monetaria = generics;
            if(context.Entry(monetaria).State == System.Data.Entity.EntityState.Modified)
            {
                context.SaveChanges();
                return monetaria;
            }
            else
            {
                return null;
            }

        }

        public override NecesidadesDonacionesMonetarias Crear(NecesidadesDonacionesMonetarias generics)
        {
           NecesidadesDonacionesMonetarias monetaria =  context.NecesidadesDonacionesMonetarias.Add(generics);
            context.SaveChanges();
            return monetaria;
        }

        public NecesidadesDonacionesMonetarias obtenerPorIdNecesidad(int idNecesidad)
        {
            return context.NecesidadesDonacionesMonetarias.Where(o=> o.IdNecesidad == idNecesidad).FirstOrDefault();
        }

        public override NecesidadesDonacionesMonetarias ObtenerPorID(int id)
        {
            return context.NecesidadesDonacionesMonetarias.Find(id);
        }
    }
}
