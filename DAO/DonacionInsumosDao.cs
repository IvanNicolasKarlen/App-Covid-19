using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Context;
using Entidades.Views;
using Entidades;
using DAO.Abstract;

namespace DAO
{
    public class DonacionInsumosDao : Crud<DonacionesMonetarias>
    {
        public DonacionInsumosDao(TpDBContext context) : base(context)
        {

        }

        public override DonacionesMonetarias Actualizar(DonacionesMonetarias generics)
        {
            throw new NotImplementedException();
        }

        public override DonacionesMonetarias Crear(DonacionesMonetarias generics)
        {
            throw new NotImplementedException();
        }

        public override DonacionesMonetarias ObtenerPorID(int id)
        {
            throw new NotImplementedException();
        }

        public List<NecesidadesDonacionesInsumos> BuscarPorId(int IdNecesidad)
        {
            List<NecesidadesDonacionesInsumos> listaObtenida = context.NecesidadesDonacionesInsumos.Where(o => o.IdNecesidad == IdNecesidad).ToList();
            return listaObtenida;
        }

    }
}
