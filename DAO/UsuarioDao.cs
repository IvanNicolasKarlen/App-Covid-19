using DAO.Context;
using Entidades;
using Entidades.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace DAO
{
    public class UsuarioDao : Crud<Usuarios> //Uso de Generics
    {
        public Usuarios obtenerUsuarioPorEmail(string email)
        {
            Usuarios usuario = context.Usuarios.Where(k => k.Email == email).FirstOrDefault();
            return usuario;
        }

        public Usuarios obtenerUsuarioPorCodigoDeActivacion(string token)
        {
            Usuarios usuario = context.Usuarios.Where(k => k.Token == token).FirstOrDefault();
            return usuario;
        }

        public override Usuarios Crear(Usuarios usuario)
        {
            Usuarios usuarioGuardado = context.Usuarios.Add(usuario);
            int valor = context.SaveChanges();

            if (valor >= 0)
            {
                return usuarioGuardado;
            }
            else
            {
                return null;
            }

        }
        public override Usuarios Actualizar(Usuarios usuarioActualizado)
        {
            Usuarios usuarioObtenido = obtenerUsuarioPorEmail(usuarioActualizado.Email);
            usuarioObtenido.Activo = usuarioActualizado.Activo;
            usuarioObtenido.Apellido = usuarioActualizado.Apellido;
            usuarioObtenido.Foto = usuarioActualizado.Foto;
            usuarioObtenido.Nombre = usuarioActualizado.Nombre;
            usuarioObtenido.UserName = usuarioActualizado.UserName;
            usuarioObtenido.Denuncias = usuarioActualizado.Denuncias;
            usuarioObtenido.DonacionesInsumos = usuarioActualizado.DonacionesInsumos;
            usuarioObtenido.DonacionesMonetarias = usuarioActualizado.DonacionesMonetarias;
            usuarioObtenido.Necesidades = usuarioActualizado.Necesidades;
            usuarioObtenido.NecesidadesValoraciones = usuarioActualizado.NecesidadesValoraciones;

            int result = context.SaveChanges();

            if(result < 0)
            {
                return null;
            }
            return usuarioObtenido;
        }

        public override Usuarios ObtenerPorID(int idSession)
        {
            Usuarios usuarioObtenido = context.Usuarios.Find(idSession);
            return usuarioObtenido;
        }


        public List<Usuarios> listadoUsuariosActivos()
        {
            List<Usuarios> listadoUsuarios = context.Usuarios.Where(a => a.Activo == true).ToList();
            return listadoUsuarios;
        }


    }
}
