using DAO.Abstract;
using DAO.Context;
using Entidades;
using Entidades.Enum;
using System.Collections.Generic;
using System.Linq;

namespace DAO
{
    public class DenunciasDao : Crud<Denuncias> //Uso de Generics
    {
        public DenunciasDao(TpDBContext context) : base(context)
        {

        }
        public List<Denuncias> ObtenerDenunciasEnRevision()
        {
            List<Denuncias> listaObtenida = context.Denuncias.Where(o => o.Necesidades.Estado == (int)TipoEstadoNecesidad.Revision).ToList();
            return listaObtenida;
        }


        public override Denuncias ObtenerPorID(int idDenuncia)
        {
            Denuncias denunciaObtenida = context.Denuncias.Find(idDenuncia);
            return denunciaObtenida;
        }

        public override Denuncias Actualizar(Denuncias denuncia)
        {
            Denuncias denunciaObtenida = ObtenerPorID(denuncia.IdDenuncia);
            denunciaObtenida.Estado = denuncia.Estado;

            context.SaveChanges();
            return denunciaObtenida;
        }

        public override Denuncias Crear(Denuncias denuncia)
        {
            Denuncias d = context.Denuncias.Add(denuncia);
            context.SaveChanges();
            return d;
        }

        public void Eliminar(Denuncias item)
        {
            context.Denuncias.Remove(item);
            context.SaveChanges();
        }

        public List<MotivoDenuncia> ObtenerMotivosDenuncia()
        {
            return context.MotivoDenuncia.ToList();
        }
        public List<Denuncias> ObtenerTodasLasDenuncias()
        {
            List<Denuncias> listaObtenida = context.Denuncias.ToList();
            return listaObtenida;
        }
    }
}
