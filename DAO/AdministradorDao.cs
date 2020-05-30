using DAO.Context;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class AdministradorDao
    {
        TpDBContext context = new TpDBContext();

        public List<Denuncias> obtenerDenuncias()
        {
            List<Denuncias> listaObtenida = context.Denuncias.ToList();
            return listaObtenida;
        }
    }
}
