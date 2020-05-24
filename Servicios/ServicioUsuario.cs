using DAO;
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

namespace Servicios
{
    public class ServicioUsuario
    {
        public Usuarios asignoDatosAUsuarioDelRegistro(VMRegistro registro)
        {
           

            Usuarios usuario = new Usuarios()
            {
                Email = registro.Email,
                Password = EncriptarPassword.GetSha256(registro.Password),
                TipoUsuario = 1,
                Activo = false,
                FechaCracion = DateTime.Now,
                FechaNacimiento = registro.FechaNacimiento.AddHours(11).AddMinutes(04).AddSeconds(04)

            };

            return usuario;
        }

        public Usuarios asignoDatosAUsuarioDelLogin(VMLogin login)
        {
            Usuarios usuario = new Usuarios();
            usuario.Email = login.Email;
            usuario.Password = login.Password;
            return usuario;
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
            if (usuario.Nombre == null | usuario.Apellido == null | usuario.FechaNacimiento == null | usuario.Foto == null)
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
                int maxLenght = random.Next(10, 30);

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
            string emailEquipoCrear = "Equipoayudar@gmail.com";
            string host = "smtp.gmail.com";

            MailMessage email = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            // A quien va dirigido
            email.To.Add(new MailAddress(usuario.Email)); //En Gmail: aca va una cuenta @gmail.com
            // Quien se lo envia
            email.From = new MailAddress(emailEquipoCrear); // Para enviar por gmail: Aca tiene que ir una cuenta de @gmail.com
            // Titulo del mensahe
            email.Subject = "Codigo de seguridad para activar mi cuenta";
            // Caracteres en UTF - 8 
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            // Cuerpo del mensaje
            email.Body = " <h1> Bienvenido a nuestro sitio web Ayudar </h1> <p> Para activar tu email: " + usuario.Email + " tenes que usar ingresar al siguiente enlace: <h3><b>  https://localhost:44303/Home/ActivarMiCuenta?token=" + usuario.Token + "  </br> <h4> Equipo Ayudar - 2020 </h4> <br>";
            // Aca activo que acepte etiquetes html en el mensaje
            email.IsBodyHtml = true;
            // El envio tiene prioridad normal
            email.Priority = MailPriority.Normal;

            //Protocolo de mensajeria hecho por gmail
            smtp.Host = host; //Para enviar por gmail: smtp.gmail.com / Outlook: smtp.live.com
            //Puerto utilizado, recomendado
            smtp.Port = 587; /*SMTP | Port 587 (Transporte inseguro, pero se puede actualizar a una conexión segura usando STARTTLS)
                               SMTP | Port 465 (Transporte Seguro - función SSL habilitada) relentizó demaciado la app, entro en un bucle.
                              
                                INFO: El puerto 587 es un puerto alternativo altamente recomendado, porque los ISP (proveedores de Internet por sus
                               siglas en Inglés) suelen bloquear el puerto 25. Asegúrate de que has habilitado el STARTTLS al usar el puerto 587.*/
            // SSl disponible
            smtp.EnableSsl = true;
            // No tenemos credenciales por default
            smtp.UseDefaultCredentials = false;
            //Asigno el email y password utilizados para este caso
            smtp.Credentials = new NetworkCredential(emailEquipoCrear, "Aguayodelgadoirachetakarlen2020");      //Para enviar por gmail: cuenta @gmail.com

            string output;
            try
            {
                //Envio del mensaje
                smtp.Send(email);
                email.Dispose();
                //Asigno un ok para asegurarme en el servicio utilizado de que se envio el mensaje
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
            UsuarioDao usuarioDao = new UsuarioDao();

            Usuarios usuarioObtenido = usuarioDao.obtenerUsuarioPorEmail(usuario.Email);

            //Si no se encontro usuario, el email es nuevo
            if (usuarioObtenido == null)
            {
                return TipoEmail.EmailNuevo;
            }
            else if (!usuarioObtenido.Activo) //Si el email aun no fue activo, entonces ya fue solicitado
            {
                return TipoEmail.EmailSolicitado;
            }
            else //Y sino, el email ya esta anotado
            {
                return TipoEmail.EmailActivo;
            }

        }

        public Usuarios ValidarCodigoDeActivacion(Usuarios usuario)
        {
            bool existeCodigo = true;
            UsuarioDao usuarioDao = new UsuarioDao();
            Usuarios usuarioObtenido = new Usuarios();

            ServicioUsuario servicioUsuario = new ServicioUsuario();


            do
            {
                usuario.Token = servicioUsuario.CodigoDeActivacion();

                usuarioObtenido = usuarioDao.obtenerUsuarioPorCodigoDeActivacion(usuario.Token);

                if (usuarioObtenido == null)
                {
                    usuarioObtenido = usuario;
                    existeCodigo = false;
                }
                else
                {
                    existeCodigo = true;
                }
            } while (existeCodigo != false);


            return usuarioObtenido;
        }


        public int registrarUsuario(Usuarios usuario)
        {
            UsuarioDao usuarioDao = new UsuarioDao();

            //Validamos que se cree un codigo unico para cada usuario y que no se repita
            Usuarios usuarioObtenido = ValidarCodigoDeActivacion(usuario);

            //Save usuario
            Usuarios usuarioGuardado = usuarioDao.guardarUsuario(usuarioObtenido);

            return usuarioGuardado.IdUsuario;
        }

        public string validoQueExistaEsteUsuario(Usuarios usuario)
        {
            UsuarioDao usuarioDao = new UsuarioDao();

            Usuarios usuarioObtenido = usuarioDao.obtenerUsuarioPorEmail(usuario.Email);
            if (usuarioObtenido == null)
            {
                return null;
            }
            String passwordEncriptada = EncriptarPassword.GetSha256(usuario.Password);
            if (usuarioObtenido.Password == passwordEncriptada)
            {
                return "ok";
            }

            return "incorrecto";
        }

        public bool validacionDelCodigoDeVerificacionJuntoAlEmail(VMDatosDeVerificacionDeUsuario datosIngresados)
        {
            Usuarios usuario = new Usuarios();

            //ToDo: Obtener objeto Usuario de la bd por el email ingresado y token ingresados

            if (usuario != null)
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
