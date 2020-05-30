using Entidades;
using Entidades.Views;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCovid19.Utilities;

namespace WebCovid19.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexLogueado()
        {
            ServicioNecesidad servicioNecesidad = new ServicioNecesidad();
            List<Necesidades> todasLasNecesidades = servicioNecesidad.listadoDeNecesidades();
            return View(todasLasNecesidades);
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
                else
                {
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
                else if (emailIngresado == TipoEmail.EmailNuevo)
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
                if (usuarioExistente == "incorrecto")
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

                    //Validar si es un Usuario o un Administrador
                    TipoUsuario tipoUsuario = servicioUsuario.tipoDeUsuario(usuario);
                    if (tipoUsuario == TipoUsuario.Usuario)
                    {
                        return RedirectToAction("IndexLogueado");
                    }
                    else
                    {
                        return RedirectToAction("Administrador");
                    }
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

            //ToDo: obtener usuario logueado y pasarselo aca
            Usuarios usuarioSession = new Usuarios();
            return View(usuarioSession);
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
                    //ToDo: obtener usuario logueado y pasarselo aca
                    Usuarios usuarioSession = new Usuarios();
                    return View("Perfil", usuarioSession);
                }

                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    //TODO: Agregar validacion para confirmar que el archivo es una imagen - UsuarioController
                    //creo un nombre significativo en este caso apellidonombre pero solo un caracter del nombre, ejemplo BatistutaG
                    string nombreSignificativo = perfil.Apellido + perfil.Nombre + Session["Email"];
                    //Guardar Imagen
                    string pathRelativoImagen = ImagenesUtil.Guardar(Request.Files[0], nombreSignificativo);
                    perfil.Foto = pathRelativoImagen;
                }

                ServicioUsuario servicioUsuario = new ServicioUsuario();

                //Asigno datos obtenidos del formulario a usuario
                Usuarios usuarioPerfil = servicioUsuario.asignoDatosAUsuarioDelPerfil(perfil);
                //ToDo: EMAIL DE PRUEBA 
                usuarioPerfil.Email = "Ivo@gmail.com";

                bool actualizado = servicioUsuario.completoDatosDeMiPerfil(usuarioPerfil);

                if (!actualizado)
                {
                    ViewData.Add("mensajeError", "Error: No se ha podido guardar los datos, intentelo nuevamente");
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

            //ToDo: obtener usuario logueado y pasarselo aca
            Usuarios usuarioSessionPerfil = new Usuarios();
            return View("Perfil", usuarioSessionPerfil);
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