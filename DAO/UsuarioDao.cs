using DAO.Context;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class UsuarioDao
    {
        TpDBContext context = new TpDBContext();

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

        public Usuarios guardarUsuario(Usuarios usuario)
        {
            Usuarios usuarioGuardado = context.Usuarios.Add(usuario);
            context.SaveChanges();
            return usuarioGuardado;
        }

        public int actualizarDatosDeUsuario(Usuarios usuarioActualizado)
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
            return result;
        }

        public List<Usuarios> listadoUsuariosActivos()
        {
            List<Usuarios> listadoUsuarios = context.Usuarios.Where(a => a.Activo == true).ToList();
            return listadoUsuarios;
        }

        
    }
}
