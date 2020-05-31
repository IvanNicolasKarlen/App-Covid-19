using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interface
{
    public interface Icrud<T> where T : class
    {
        T Guardar(T generics);
        int Actualizar(T generics);
        T ObtenerPorID(int generics);

    }
}
