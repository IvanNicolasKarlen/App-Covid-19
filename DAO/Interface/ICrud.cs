namespace DAO.Interface
{
    public interface ICrud<T> where T:class
    {
          T Guardar(T generics);
         
          T ObtenerPorID(int id);
    }
}
