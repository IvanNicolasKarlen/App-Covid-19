using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using DAO;
using Entidades.Metadata;

namespace Servicios
{
    public class ServicioNecesidadesMonetarias
    {
        NecesidadesDAO necesidadesDAO = new NecesidadesDAO();
        NecesidadesDonacionesMonetariasDAO MonetariasDAO = new NecesidadesDonacionesMonetariasDAO();
        ServicioNecesidad servicioNecesidad = new ServicioNecesidad();
        public void GuardarMonetarias(NecesidadesDonacionesMonetariasMetadata monetariaMeta)
        {
            NecesidadesDonacionesMonetarias monetaria = new NecesidadesDonacionesMonetarias()
            {
                IdNecesidad = monetariaMeta.IdNecesidad,
                Necesidades = monetariaMeta.Necesidades,
                Dinero = monetariaMeta.Dinero,
                CBU = monetariaMeta.CBU
            };
            MonetariasDAO.Crear(monetaria);
        }

        public NecesidadesDonacionesMonetarias obtenerPorIdNecesidad(int idNecesidad)
        {
            Necesidades necesidadBD = necesidadesDAO.ObtenerPorID(idNecesidad);
            Necesidades necesidadValoracion = servicioNecesidad.calcularValoracion(necesidadBD);
            Necesidades necesidadActualizada = necesidadesDAO.Actualizar(necesidadValoracion);
            NecesidadesDonacionesMonetarias monetariasDAO = MonetariasDAO.obtenerPorIdNecesidad(necesidadActualizada.IdNecesidad);
            monetariasDAO.Necesidades.Valoracion = necesidadValoracion.Valoracion;
            return monetariasDAO;
        }
    }
}
