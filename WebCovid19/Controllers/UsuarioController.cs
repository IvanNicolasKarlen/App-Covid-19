using Entidades;
using Entidades.Views;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCovid19.Controllers
{
    public class UsuarioController : Controller
    {

        /*
                public ActionResult Perfil()
                {
                    Session["url"] = Request["url"];
                    if (Session["Email"] as string == "") 
                    {
                        return RedirectToAction("Login","Home");
                    }
                    return View();
                }
        */

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexLogueado()
        {
            return View();
        }

        public ActionResult Salir()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index");
        }

        public ActionResult Registro()
        {
            VMRegistro registro = new VMRegistro();
            return View(registro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro(VMRegistro registro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                ServicioUsuario servicioUsuario = new ServicioUsuario();
                Usuarios usuario = new Usuarios();

                //Asigno datos obtenidos del formulario a usuario
                usuario = servicioUsuario.asignoDatosAUsuarioDelRegistro(registro);

                //Validar si el email es un email nuevo o si ya fue registrado
                TipoEmail emailIngresado = servicioUsuario.ValidoEstadoEmail(usuario);

                //Esta condicion es por si se le envie la activacion, elimina el mensaje, y quiere recuperar su activacion.
                if (emailIngresado == TipoEmail.EmailNuevo)
                {
                    if (servicioUsuario.registrarUsuario(usuario) >= 0)
                    {
                        ViewData.Add("mensajeAdvertencia", "Te hemos enviado un email por Gmail con su clave de activación");

                        string mensajeEnviado = servicioUsuario.EnviarCodigoPorEmail(usuario);

                        if (mensajeEnviado != "Ok")
                        {
                            ViewData.Add("mensajeError", mensajeEnviado);
                        }
                    }
                    else
                    {
                        ViewData.Add("mensajeError", "Ha ocurrido un error al registrarse, por favor intentelo nuevamente");
                    }
                }
                else{
                    ViewData.Add("mensajeError", "Ya existe una cuenta con ese email");
                }
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return View();
        }

        public ActionResult ReenvioDeCodigo()
        {
            VMReenvioDeEmail email = new VMReenvioDeEmail();
            return View(email);
        }

        [HttpPost]
        public ActionResult ReenvioDeCodigo(VMReenvioDeEmail emailRecibido)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                ServicioUsuario servicioUsuario = new ServicioUsuario();
                Usuarios usuarioObtenido = new Usuarios(); 
                usuarioObtenido.Email = emailRecibido.Email;

                //Validar si el email es un email nuevo o si ya fue registrado
                TipoEmail emailIngresado = servicioUsuario.ValidoEstadoEmail(usuarioObtenido);

                if (emailIngresado == TipoEmail.EmailSolicitado)
                {
                    //Se le envia nuevamente su token al usuario ya registrado
                    string mensajeEnviado = servicioUsuario.ReenviarEmail(usuarioObtenido);

                    if (servicioUsuario.ReenviarEmail(usuarioObtenido) != "Ok")
                    {
                        ViewData.Add("mensajeError", mensajeEnviado);
                    }
                    else
                    {
                        ViewData.Add("mensajeAdvertencia", "Te hemos enviado nuevamente un email por Gmail con su clave de activación");
                    }
                }
                else if(emailIngresado == TipoEmail.EmailNuevo)
                {
                    //Aun no se registro
                    ViewData.Add("mensajeAdvertencia", "Todavia no se ha registrado un usuario con ese email");
                }
                else
                {   //Usuario ya activo
                    ViewData.Add("mensajeError", "Ya existe una cuenta activa con ese email");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            VMLogin login = new VMLogin();
            return View(login);
        }


        [HttpPost]
        public ActionResult Login(VMLogin login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                ServicioUsuario servicioUsuario = new ServicioUsuario();
                Usuarios usuario = new Usuarios();

                //Asigno datos obtenidos del formulario a usuario
                usuario = servicioUsuario.asignoDatosAUsuarioDelLogin(login);

                //Validar si existe este usuario
                string usuarioExistente = servicioUsuario.validoQueExistaEsteUsuario(usuario);
                if (usuarioExistente == null)
                {
                    ViewData.Add("mensajeError", "No existe ese email, debera registrarse primero");
                    return View();
                }
                else if (usuarioExistente == "incorrecto")
                {
                    ViewData.Add("mensajeError", "La contraseña ha sido incorrecta");
                    return View();
                }
                else if (usuarioExistente == "ok")
                {
                    //Validar si esta activo o no
                    TipoEmail estadoEmail = servicioUsuario.ValidoEstadoEmail(usuario);
                    if (estadoEmail != TipoEmail.EmailActivo)
                    {
                        ViewData.Add("mensajeAdvertencia", "Su usuario está inactivo. Actívelo desde el email recibido");
                        return View();
                    }



                    /*      bool bandera = true;
            if (bandera)
            {
                Session["Email"] = login.Email;

                string url = Session["url"] as string;
                if (url != "")
                {
                    return Redirect(url);

                }
                return RedirectToAction("IndexLogueado","Usuario");
            }
            /*-----------*/

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }


            return RedirectToAction("IndexLogueado");
        }

        public ActionResult Perfil()
        {
            ViewData.Add("mensajeInfo", "Debe completar sus datos para poder Crear Necesidades");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarPerfil(VMPerfil perfil)
        {
            //toDo: Uso de session para saber a que perfil pertenece el usuario logueado
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Perfil");
                }
                ServicioUsuario servicioUsuario = new ServicioUsuario();

                //Asigno datos obtenidos del formulario a usuario
                Usuarios usuarioPerfil = servicioUsuario.asignoDatosAUsuarioDelPerfil(perfil);
                /*DE PRUEBA*/usuarioPerfil.Email = "Ivan3@hotmail.com";
                
                //Obtengo el objeto usuario con los datos anteriores para agregarle los nuevos datos
                Usuarios usuarioObtenido = servicioUsuario.obtenerUsuarioPorEmail(usuarioPerfil.Email);
                
                //Agrego los datos faltantes al usuario obtenido de la bd
                Usuarios usuarioActualizado = servicioUsuario.asignoDatosFaltantesAUsuarioDePerfil(usuarioPerfil, usuarioObtenido);

                bool actualizado = servicioUsuario.actualizoDatosDelPerfilDelUsuario(usuarioActualizado);

                if (!actualizado)
                {
                    ViewData.Add("mensajeError", "Error: No se ha podido guardar los datos, intentelo nuevamente");
                    return View("Perfil");
                }
                else
                {
                    ViewData.Add("mensajeCorrecto", "¡Datos guardados correctamente!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return View("Perfil");
        }

        public ActionResult ActivarMiCuenta(string token)
        {
            VMDatosDeVerificacionDeUsuario vmDatosDeVerificacion = new VMDatosDeVerificacionDeUsuario();

            if (!ModelState.IsValid)
            {
                return View();
            }
            ServicioUsuario servicioUsuario = new ServicioUsuario();

            //Validar que el email coincida con el codigo
            bool estado = servicioUsuario.validacionDelCodigoDeVerificacionJuntoAlEmail(token);
            if (!estado)
            {
                ViewData.Add("mensajeError", "No encontramos un email con esa clave de verificacion");
                return View("Login");
            }
            else
            {
                ViewData.Add("mensajeCorrecto", "¡Has activado tu cuenta exitosamente! Logueate asi podés ingresar");
            }

            return View("Login");
        }



        public ActionResult Administrador()
        {
            return View();
        }

    }
}