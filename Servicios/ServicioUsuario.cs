using Entidades;
using Entidades.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using WebCovid19.Models.Views;

namespace WebCovid19.Services
{
    public class ServicioUsuario
    {
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

        public string CodigoDeActivacion()
        {
            try
            {   //Generar un numero random para enviar el token al email del usuario
                Random random = new Random();
                const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-/$%&!";
                int longitud = allowedChars.Length;
                string res = "";
                int maxLenght = random.Next(10, 40);

                for (int i = 0; i < maxLenght; i++)
                {
                    res += allowedChars[random.Next(longitud)];
                }

                return res;
            }
            catch (Exception)
            {
                throw new Exception("No se puede generar una cadena aleatoria");
            }
        }

        public String EnviarCodigoPorEmail(Usuarios usuario)
        {
            MailMessage email = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            email.To.Add(new MailAddress(usuario.Email));
            email.From = new MailAddress("EquipoAyudar@gmail.com");
            email.Subject = "Codigo de seguridad para activar mi cuenta";
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            email.Body = " <h1> Bienvenido a nuestro sitio web Ayudar </h1> <p> Para activar tu email: " + usuario.Email + " tenes que usar el siguiente codigo: <h3><b>" + usuario.Token + "</b></h3></br> Podes activar tu cuenta desde aca: https://localhost:44303/Home/CodigoDeVerificacion  </br> <h4> Equipo Ayudar - 2020 </h4>";
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587; /*SMTP | Port 587 (Transporte inseguro, pero se puede actualizar a una conexión segura usando STARTTLS)
                               SMTP | Port 465 (Transporte Seguro - función SSL habilitada) relentizó demaciado la app, entro en un bucle.
                              
                                INFO: El puerto 587 es un puerto alternativo altamente recomendado, porque los ISP (proveedores de Internet por sus
                               siglas en Inglés) suelen bloquear el puerto 25. Asegúrate de que has habilitado el STARTTLS al usar el puerto 587.*/
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("EquipoAyudar@gmail.com", "Aguayodelgadoirachetakarlen2020");

            string output;
            try
            {
                smtp.Send(email);
                email.Dispose();
                output = "Ok";
            }
            catch (Exception ex)
            {
                output = "Error enviando correo electrónico: " + ex.Message;
            }

             return output;
        }
        


        public TipoEmail ValidoEstadoEmail(Usuarios usuario)
        {

            Usuarios usuarioObtenido = null;

            TipoEmail estadoDelEmail = new TipoEmail();

            //Si no se encontro usuario, el email es nuevo
            if(usuarioObtenido == null)
            {
                estadoDelEmail = TipoEmail.EmailNuevo;
            }
            else if(!usuarioObtenido.Activo) //Si el email aun no fue activo, entonces ya fue solicitado
            {
                estadoDelEmail = TipoEmail.EmailSolicitado;
            }
            else //Y sino, el email ya esta anotado
            {
                return TipoEmail.EmailActivo;
            }


            //Enviar email al usuario
            

            return estadoDelEmail;
        }


        public int registrarUsuario(Usuarios usuario)
        {
            //Asigno valor del token al usuario
            usuario.Token = CodigoDeActivacion();
            usuario.Activo = false;

            //Save usuario
            return 0; //id usuario
        }

        public bool validoQueExistaEsteUsuario(Usuarios usuario)
        {
            if(usuario == null)
            {
                return false;
            }

            return true;
        }

        public bool validacionDelCodigoDeVerificacionJuntoAlEmail(VMDatosDeVerificacionDeUsuario datosIngresados)
        {
            Usuarios usuario = new Usuarios();

            //ToDo: Obtener objeto Usuario de la bd por el email ingresado y token ingresados
            
            if(usuario != null )
            {
                return true;
            }

            return false;
        }


        public void ponerEstadoActivoAlUsuario(VMDatosDeVerificacionDeUsuario datosIngresados)
        {
            Usuarios usuarioObtenido = new Usuarios();
            //Obtener al usuario por email y token
            usuarioObtenido.Activo = true;

            //Update usuario
        }

    }
}