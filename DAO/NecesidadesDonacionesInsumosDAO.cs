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
    public class NecesidadesDonacionesInsumosDAO : Crud<NecesidadesDonacionesInsumos>
    {

        public NecesidadesDonacionesInsumosDAO(TpDBContext context) : base(context)
        {
        }

        public override NecesidadesDonacionesInsumos Actualizar(NecesidadesDonacionesInsumos generics)
        {
            throw new NotImplementedException();
        }

        public override NecesidadesDonacionesInsumos Crear(NecesidadesDonacionesInsumos generics)
        {
            throw new NotImplementedException();
        }

        public override NecesidadesDonacionesInsumos ObtenerPorID(int id)
        {
            throw new NotImplementedException();
        }

        public List<NecesidadesDonacionesInsumos> BuscarPorId(int IdNecesidad)
        {
            List<NecesidadesDonacionesInsumos> listaObtenida = context.NecesidadesDonacionesInsumos.Where(o => o.IdNecesidad == IdNecesidad).ToList();
            return listaObtenida;
        }

        public NecesidadesDonacionesInsumos ObtenerNecesidadDonacionInsumosPorId(int IdNecesidadDonacionInsumo)
        {
            NecesidadesDonacionesInsumos DonacionesPorId = context.NecesidadesDonacionesInsumos.Find(IdNecesidadDonacionInsumo);
            return DonacionesPorId;
        }

        public NecesidadesDonacionesInsumos BuscarNecesidadesDonacionIPorId(int IdNecesidadDonacionInsumo)
        {
            NecesidadesDonacionesInsumos listaNdi = context.NecesidadesDonacionesInsumos.Find(IdNecesidadDonacionInsumo);
            return listaNdi;
        }

    }
}
