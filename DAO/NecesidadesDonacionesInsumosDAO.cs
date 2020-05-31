using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Context;
using Entidades;
using Entidades.Abstract;

namespace DAO
{
    public class NecesidadesDonacionesInsumosDAO : Crud<NecesidadesDonacionesInsumos> //Uso de Generics
    {
        public override NecesidadesDonacionesInsumos Actualizar(NecesidadesDonacionesInsumos generics)
        {
            throw new NotImplementedException();
        }

        public override NecesidadesDonacionesInsumos Crear(NecesidadesDonacionesInsumos generics)
        {
            throw new NotImplementedException();
        }

        public void GuardarInsumo(NecesidadesDonacionesInsumos insumo)
        {
            context.NecesidadesDonacionesInsumos.Add(insumo);
        }

        public override NecesidadesDonacionesInsumos ObtenerPorID(int generics)
        {
            throw new NotImplementedException();
        }
    }
}
