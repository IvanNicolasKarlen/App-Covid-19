using System;
using Dao.Abstract;
using Entidades;

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
