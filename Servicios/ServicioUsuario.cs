using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCovid19.Models.Views;

namespace WebCovid19.Services
{
    public class ServicioUsuario
    {

        public bool ValidoUsuarioActivo(Usuarios usuario)
        {
            //Obtengo el usuario a traves del email

            if (!usuario.Activo)
            {
                return false;
            }

            return true;
        }

        public bool validoQueExistaEsteUsuario(Usuarios usuario)
        {
            //Obtengo el objeto usuario con ambos datos

            if(usuario is null)
            {
                return false;
            }
            
            return true;
        }

        public Usuarios asignoDatosAUsuarioDelRegistro(VMRegistro registro)
        {
            Usuarios usuario = new Usuarios();

            usuario.Email = registro.Email;
            usuario.FechaNacimiento = registro.FechaNacimiento;
            usuario.Password = registro.Password;
            usuario.RepeatPassword = registro.RepeatPassword;

            return usuario;
        }

        public Usuarios asignoDatosAUsuarioDelLogin(VMLogin login)
        {
            Usuarios usuario = new Usuarios();
            usuario.Email = login.Email;
            usuario.Password = login.Password;
            return usuario;
        }

        public bool datosRecibidosDelFormularioRegistro(Usuarios usuario)
        {
            if (usuario.Email == null | usuario.Password == null | usuario.RepeatPassword == null | usuario.FechaNacimiento == null)
            {
                if (usuario.Password != usuario.RepeatPassword)
                {
                    return false;
                }
                return false;
            }

            return true;
        }

        public bool datosRecibidosDelFormularioLogin(Usuarios usuario)
        {
            if(usuario.Email==null | usuario.Password== null)
            {
                return false;
            }

            return true;
        }

        public Usuarios asignoDatosAUsuarioDelPerfil(VMPerfil perfil)
        {
            Usuarios usuario = new Usuarios();
            usuario.Nombre = perfil.Nombre;
            usuario.Apellido = perfil.Apellido;
            usuario.FechaNacimiento = perfil.FechaNacimiento;
            usuario.Foto = perfil.Foto;
            return usuario;
        }

        public bool datosRecibidosDelFormularioPerfil(Usuarios usuario)
        {
            if(usuario.Nombre==null | usuario.Apellido== null | usuario.FechaNacimiento==null | usuario.Foto==null)
            {
                return false;
            }

            return true;
        }
    }
}