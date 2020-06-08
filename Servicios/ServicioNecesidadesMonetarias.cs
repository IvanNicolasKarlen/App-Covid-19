using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using DAO;
using Entidades.Metadata;
using DAO.Context;

namespace Servicios
{
    public class ServicioNecesidadesMonetarias
    {
        NecesidadesDAO necesidadesDAO;
        NecesidadesDonacionesMonetariasDAO MonetariasDAO;
        ServicioNecesidad servicioNecesidad;

        public ServicioNecesidadesMonetarias(TpDBContext context)
        {
             necesidadesDAO = new NecesidadesDAO(context);
             MonetariasDAO = new NecesidadesDonacionesMonetariasDAO(context);
             servicioNecesidad = new ServicioNecesidad(context);
        }

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
            NecesidadesDonacionesMonetarias monetariasDAO = MonetariasDAO.obtenerPorIdNecesidad(necesidadBD.IdNecesidad);
            return monetariasDAO;
        }
    }
}
