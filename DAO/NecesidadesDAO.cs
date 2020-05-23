/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCovid19;

namespace DAO
{
    public class NecesidadesDAO
    {
        TpDBContext context;
        public Necesidades CrearNecesidades(Necesidades necesidadAGuardar)
        {
            //ToDo: Verificar conexion a la bd, xq esto devuelve null
            Necesidades necesidades = context.Necesidades.Add(necesidadAGuardar);
            context.SaveChanges();
            return necesidades;
        }
    }
}
*/