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
    public class ServicioNecesidadesInsumos
    {
        NecesidadesDonacionesInsumosDAO insumosDAO = new NecesidadesDonacionesInsumosDAO();
        public void GuardarInsumos(NecesidadesDonacionesInsumosMetadata insumoMeta)
        {
            NecesidadesDonacionesInsumos insumo = new NecesidadesDonacionesInsumos()
            {
                IdNecesidad = insumoMeta.IdNecesidad,
                Necesidades = insumoMeta.Necesidades,
                Nombre = insumoMeta.Nombre,
                Cantidad = insumoMeta.Cantidad
            };
            insumosDAO.Crear(insumo);
        }
    }
}
