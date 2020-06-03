using DAO.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Abstract
{
    public abstract class Crud<T> where T : class
    {
        public static TpDBContext context;
        

    public Crud()
    {
            if (context == null)
            {
                context = new TpDBContext();
            }
    }
        public abstract T Crear(T generics);
        public abstract T Actualizar(T generics);
        public abstract T ObtenerPorID(int id);
    }
}
