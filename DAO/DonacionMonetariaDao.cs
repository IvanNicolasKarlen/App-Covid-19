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
    public class DonacionMonetariaDao : Crud<DonacionesMonetarias> //Uso de Generics
    {
        public override DonacionesMonetarias Actualizar(DonacionesMonetarias generics)
        {
            throw new NotImplementedException();
        }

        public override DonacionesMonetarias Crear(DonacionesMonetarias generics)
        {
            throw new NotImplementedException();
        }

        public override DonacionesMonetarias ObtenerPorID(int generics)
        {
            throw new NotImplementedException();
        }

        public List<DonacionesMonetarias> ObtenerPorId(int IdDonacionMonetaria)
        {
            List<DonacionesMonetarias> listarCantidadNecesariaADonar =
            context.DonacionesMonetarias.Where(a => a.IdDonacionMonetaria == IdDonacionMonetaria).ToList();

            return listarCantidadNecesariaADonar;
        }

        public NecesidadesDonacionesMonetarias CantidadSolicitada(int IdNecesidadDonacionMonetaria)
        {
            NecesidadesDonacionesMonetarias traerDineroPorId = 
            context.NecesidadesDonacionesMonetarias.Find(IdNecesidadDonacionMonetaria);
            return traerDineroPorId;
        }
    }
}
