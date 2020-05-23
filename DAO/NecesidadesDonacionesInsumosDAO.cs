using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Context;
using Entidades;
namespace DAO
{
    public class NecesidadesDonacionesInsumosDAO
    {
        TpDBContext context = new TpDBContext();
        public void GuardarInsumo(NecesidadesDonacionesInsumos insumo)
        {
            context.NecesidadesDonacionesInsumos.Add(insumo);
        }
    }
}
