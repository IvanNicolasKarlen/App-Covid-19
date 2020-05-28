using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Views
{
    public interface ICRUD<T> where T : class
    {
        T Guardar(T generics);
        int Actualizar(T generics);
        T ObtenerPorID(int generics);

    }
}
