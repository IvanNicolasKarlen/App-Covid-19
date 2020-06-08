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
    public class DenunciasDao : Crud<Denuncias> //Uso de Generics
    {
        public DenunciasDao(TpDBContext context) : base (context)
        {

        }
        public List<Denuncias> obtenerDenuncias()
        {
            List<Denuncias> listaObtenida = context.Denuncias.Where(o => o.Estado == 1).ToList();
            return listaObtenida;
        }

        public Denuncias obtenerDenunciaPorIdNecesidad(int idNecesidad)
        {
            Denuncias denunciaObtenida = context.Denuncias.Where(d => d.Necesidades.IdNecesidad == idNecesidad).FirstOrDefault();
            return denunciaObtenida;
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

        public override Denuncias Crear(Denuncias generics)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Denuncias item)
        {
            context.Denuncias.Remove(item);
            context.SaveChanges();
        }
    }
}
