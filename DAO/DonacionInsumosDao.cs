using DAO.Abstract;
using DAO.Context;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public NecesidadesDonacionesInsumos BuscarNecesidadesDonacionIPorId(int IdNecesidadDonacionInsumo)
        {
            NecesidadesDonacionesInsumos listaNdi = context.NecesidadesDonacionesInsumos.Find(IdNecesidadDonacionInsumo);
            return listaNdi;
        }
        public DonacionesInsumos GuardarInsumo(DonacionesInsumos donacionInsumo)
        {
            DonacionesInsumos donacion = context.DonacionesInsumos.Add(donacionInsumo);
            context.SaveChanges();
            return donacion;
        }

        public NecesidadesDonacionesInsumos ObtenerNecesidadDonacionInsumosPorId(int IdNecesidadDonacionInsumo)
        {
            NecesidadesDonacionesInsumos DonacionesPorId = context.NecesidadesDonacionesInsumos.Find(IdNecesidadDonacionInsumo);
            return DonacionesPorId;
        }
    }
}
