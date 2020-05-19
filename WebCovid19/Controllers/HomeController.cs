using Entidades;
using Entidades.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using WebCovid19.Models.Views;
using WebCovid19.Services;

namespace WebCovid19.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexLogueado()
        {
            return View();
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

                //Validar datos ingresados
                bool datosIngresados = servicioUsuario.datosRecibidosDelFormularioRegistro(usuario);
                if(!datosIngresados)
                {
                    ViewBag.mensajeError = "Debe ingresar sus datos en todos los campos";
                    return View();
                }


                //Validar si el email es un email nuevo o si ya fue solicitado para registrarse
                TipoEmail emailIngresado = servicioUsuario.ValidoEstadoEmail(usuario);

                //Esta condicion es por si se le envie la activacion, elimina el mensaje, y quiere recuperar su activacion.
                if(emailIngresado == TipoEmail.EmailNuevo)
                {
                    if(servicioUsuario.registrarUsuario(usuario) >= 0)
                    {
                        ViewData.Add("mensajeAdvertencia", "Te hemos enviado un email con su clave de activación");
                        return View();
                    }
                    
                }
                else if(emailIngresado == TipoEmail.EmailSolicitado)
                {
                    if (servicioUsuario.registrarUsuario(usuario) >= 0)
                    {
                        ViewData.Add("mensajeAdvertencia", "Te hemos enviado nuevamente un email con su clave de activación");
                        return View();
                    }
                }
                else
                {
                    ViewData.Add("mensajeError", "Ya existe una cuenta con ese email");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return RedirectToAction("Perfil");
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
                bool usuarioExistente = servicioUsuario.validoQueExistaEsteUsuario(usuario);
                if (!usuarioExistente)
                {
                    ViewData.Add("mensajeError", "Email o contraseña ha sido incorrecto");
                    return View();
                }

                //Validar si esta activo o no
                TipoEmail estadoEmail = servicioUsuario.ValidoEstadoEmail(usuario);
                if(estadoEmail != TipoEmail.EmailActivo)
                {
                    ViewData.Add("mensajeAdvertencia", "Su usuario está inactivo. Actívelo desde el email recibido");
                    return View();
                }

                //Validar datos ingresados
                bool datosIngresados = servicioUsuario.datosRecibidosDelFormularioLogin(usuario);
                if(!datosIngresados)
                {
                    ViewData.Add("mensajeError", "Ingrese los datos correspondientes en cada campo");
                    return View();
                }

            }catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }


            return RedirectToAction("IndexLogueado");
        }

        public ActionResult Perfil()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarPerfil(VMPerfil perfil)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Perfil");
                }
                ServicioUsuario servicioUsuario = new ServicioUsuario();
                Usuarios usuario = new Usuarios();

                //Asigno datos obtenidos del formulario a usuario
                usuario = servicioUsuario.asignoDatosAUsuarioDelPerfil(perfil);
                //Valido que los datos ingresados estén bien
                bool datosIngresados = servicioUsuario.datosRecibidosDelFormularioPerfil(usuario);
                
                if(!datosIngresados)
                {
                    ViewBag.mensajeError = "Ingrese los datos correspondientes en cada campo";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult CodigoDeVerificacion()
        {
            VMDatosDeVerificacionDeUsuario vmDatosDeVerificacion = new VMDatosDeVerificacionDeUsuario();
            return View(vmDatosDeVerificacion);
        }

        [HttpPost]
        public ActionResult CodigoDeVerificacion(VMDatosDeVerificacionDeUsuario vmDatosDeVerificacion)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            ServicioUsuario servicioUsuario = new ServicioUsuario();

            //Validar que el email coincida con el codigo
            bool estado = servicioUsuario.validacionDelCodigoDeVerificacionJuntoAlEmail(vmDatosDeVerificacion);
            if(!estado)
            {
                ViewData.Add("mensajeError", "No encontramos un email con esa clave de verificacion, " +
                    "asegurese de estar ingresando los datos correctamente");
                return View();
            }
            else 
            {
                ViewData.Add("mensajeCorrecto", "Has activado tu cuenta exitosamente, dirigite al Login asi podés ingresar");
                servicioUsuario.ponerEstadoActivoAlUsuario(vmDatosDeVerificacion);
            }
            return View();
        }




        public ActionResult Administrador()
        {
            return View();
        }


    }
}