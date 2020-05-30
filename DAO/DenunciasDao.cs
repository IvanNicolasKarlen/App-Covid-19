using DAO.Context;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DenunciasDao
    {
        TpDBContext context = new TpDBContext();

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

        public Denuncias obtenerDenunciaPorId(int idDenuncia)
        {
            Denuncias denunciaObtenida = context.Denuncias.Find(idDenuncia);
            return denunciaObtenida;
        }

        public Denuncias Actualizar(Denuncias denuncia)
        {
            Denuncias denunciaObtenida = obtenerDenunciaPorId(denuncia.IdDenuncia);
            denunciaObtenida.Estado = denuncia.Estado;
            foreach (var item in denunciaObtenida.Necesidades.Denuncias)
            {
                denunciaObtenida.Necesidades.Denuncias.Remove(item);
            }
            context.SaveChanges();
            return denunciaObtenida;
        }
    }
}
