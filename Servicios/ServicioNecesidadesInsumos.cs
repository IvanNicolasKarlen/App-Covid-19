using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using DAO;
namespace Servicios
{
    public class ServicioNecesidadesInsumos
    {
        NecesidadesDonacionesInsumosDAO insumosDAO = new NecesidadesDonacionesInsumosDAO();
        public void GuardarInsumos(NecesidadesDonacionesInsumos insumo)
        {
            insumosDAO.GuardarInsumo(insumo);
        }
    }
}
