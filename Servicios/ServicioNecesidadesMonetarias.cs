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
        NecesidadesDonacionesMonetariasDAO MonetariasDAO = new NecesidadesDonacionesMonetariasDAO();
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
    }
}
