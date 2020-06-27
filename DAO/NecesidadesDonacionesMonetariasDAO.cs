using DAO.Abstract;
using DAO.Context;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class NecesidadesDonacionesMonetariasDAO : Crud<NecesidadesDonacionesMonetarias>
    {

        public NecesidadesDonacionesMonetariasDAO(TpDBContext context) : base(context)
        {
        }

        public override NecesidadesDonacionesMonetarias Actualizar(NecesidadesDonacionesMonetarias generics)
        {
            throw new NotImplementedException();
        }

        public override NecesidadesDonacionesMonetarias Crear(NecesidadesDonacionesMonetarias generics)
        {
            throw new NotImplementedException();
        }

        public override NecesidadesDonacionesMonetarias ObtenerPorID(int id)
        {
            throw new NotImplementedException();
        }

        public NecesidadesDonacionesMonetarias ObtenerPorIdNecesidadDonacionMonetaria(int IdNecesidadDonacionMonetaria)
        {
            NecesidadesDonacionesMonetarias traerDineroPorId =
            context.NecesidadesDonacionesMonetarias.Find(IdNecesidadDonacionMonetaria);
            return traerDineroPorId;
        }
    }
}
