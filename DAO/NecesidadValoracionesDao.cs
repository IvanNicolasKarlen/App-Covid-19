using DAO.Abstract;
using DAO.Context;
using Entidades;
using System.Collections.Generic;
using System.Linq;

namespace DAO
{
    public class NecesidadValoracionesDao : Crud<NecesidadesValoraciones>
    {
        public override NecesidadesValoraciones ObtenerPorID(int idValoracion)
        {
            NecesidadesValoraciones valoracionObtenida = context.NecesidadesValoraciones.Find(idValoracion);
            return valoracionObtenida;
        }

        //toDo: Tuve que crear dos metodos CREAR por conflictos de context
        public NecesidadesValoraciones Crear(Usuarios usuario, Necesidades necesidad)
        {//toDo: Lo hago de esta manera porque sino me genera error por estar usando dos context distintos
            UsuarioDao usuarioDao = new UsuarioDao();
            NecesidadesDAO necesidadesDAO = new NecesidadesDAO();

            Usuarios usuarioBD = context.Usuarios.Find(usuario.IdUsuario);
            Necesidades necesidadBD = context.Necesidades.Find(necesidad.IdNecesidad);

            NecesidadesValoraciones necesidadesValoraciones = new NecesidadesValoraciones();
            necesidadesValoraciones.IdUsuario = usuarioBD.IdUsuario;
            necesidadesValoraciones.IdNecesidad = necesidadBD.IdNecesidad;
            necesidadesValoraciones.Usuarios = usuarioBD;
            necesidadesValoraciones.Necesidades = necesidadBD;
            necesidadesValoraciones.Valoracion = "Undefined";


            NecesidadesValoraciones valoracionGuardada = context.NecesidadesValoraciones.Add(necesidadesValoraciones);
            int resultado = context.SaveChanges();

            if (resultado < 0)
            {
                return null;
            }
            return valoracionGuardada;
        }

        public List<NecesidadesValoraciones> obtenerValoracionesDelUsuario(int idSession)
        {
            List<NecesidadesValoraciones> valoracionesObtenidas = context.NecesidadesValoraciones.Where(o => o.IdUsuario == idSession).ToList();
            return valoracionesObtenidas;
        }

        public NecesidadesValoraciones obtenerNecesidadValoracionPor_IDUsuario_e_IdNecesidad(int idUsuario, int idNecesidad)
        {
            NecesidadesValoraciones valoracionObtenida = context.NecesidadesValoraciones.Where(o => o.IdUsuario == idUsuario).Where(n => n.IdNecesidad == idNecesidad).FirstOrDefault();
            return valoracionObtenida;
        }

        public override NecesidadesValoraciones Actualizar(NecesidadesValoraciones valoracionNueva)
        {
            NecesidadesValoraciones valoracionObtenida = ObtenerPorID(valoracionNueva.IdValoracion);
            valoracionObtenida.Valoracion = valoracionNueva.Valoracion;
            context.SaveChanges();
            return valoracionObtenida;
        }

        public List<NecesidadesValoraciones> obtenerValoracionesPorIDNecesidad(int idNecesidad)
        {
            List<NecesidadesValoraciones> listadoObtenido = context.NecesidadesValoraciones.Where(o => o.IdNecesidad == idNecesidad).ToList();
            return listadoObtenido;
        }

        public override NecesidadesValoraciones Crear(NecesidadesValoraciones necesidadesValoraciones)
        {
            NecesidadesValoraciones valoracionGuardada = context.NecesidadesValoraciones.Add(necesidadesValoraciones);
            int resultado = context.SaveChanges();

            if (resultado < 0)
            {
                return null;
            }
            return valoracionGuardada;
        }
    }
}
