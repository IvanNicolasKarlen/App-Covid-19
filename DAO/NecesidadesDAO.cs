using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using DAO.Context;
namespace DAO
{
    public class NecesidadesDAO
    {
        TpDBContext context = new TpDBContext();

        public Necesidades BuscarNecesidad(int idNecesidad)
        {
           Necesidades necesidad = context.Necesidades.Find(idNecesidad);
           return necesidad;
        }
        public Necesidades CrearNecesidades(Necesidades necesidadAGuardar)
        {
            Necesidades necesidades = context.Necesidades.Add(necesidadAGuardar);
            context.SaveChanges();
            return necesidades;
        }
    }
}
