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

                //Validar si existe este usuario
                bool usuarioExistente = servicioUsuario.validoQueExistaEsteUsuario(usuario);
                if (usuarioExistente)
                {
                    ViewBag.mensajeError = "Ya existe una cuenta con ese email";
                    return View();
                }

                //Validar datos ingresados
                bool datosIngresados = servicioUsuario.datosRecibidosDelFormularioRegistro(usuario);
                if(!datosIngresados)
                {
                    ViewBag.mensajeError = "Debe ingresar sus datos en todos los campos";
                    return View();
                }

                //Agregar usuario y enviar token
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return RedirectToAction("Perfil");
        }



        [HttpGet]
        public ActionResult Login(int? id)
        {
            VMLogin login = new VMLogin();
            //Nos puede servir para que, cuando continue el login busquemos la necesidad por el id.
            ViewBag.idNecesidad = id;
            return View(login);
        }


        [HttpPost]
        public ActionResult Login(VMLogin login)
        {
            try { 

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
                if(!usuarioExistente)
                {
                    ViewBag.mensajeError = "Email o contraseña ingresados ha sido incorrecto";
                    return View();
                }

                //Validar si esta activo o no
                bool activo = servicioUsuario.ValidoUsuarioActivo(usuario);
                if(!activo)
                {
                    ViewBag.mensajeError = "Su usuario está inactivo. Actívelo desde el email recibido";
                    return View();
                }

                //Validar datos ingresados
                bool datosIngresados = servicioUsuario.datosRecibidosDelFormularioLogin(usuario);
                if(!datosIngresados)
                {
                    ViewBag.mensajeError = "Ingrese los datos correspondientes en cada campo";
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


        public ActionResult Denuncia(int? id)
        {
            Denuncias denuncia = new Denuncias();
            ServicioNecesidad servicioNecesidad = new ServicioNecesidad();

            Necesidades necesidadDenunciada = servicioNecesidad.obtenerNecesidadPorId(id);
            ViewBag.titulo = necesidadDenunciada.Nombre;
            ViewBag.idNecesidad = id;
            return View(denuncia);
        }


        [HttpGet]
        public ActionResult DonacionMonetaria()
        {
            return View();
        }



        [HttpPost]
        public ActionResult DonacionMonetaria(VMDonacionMonetaria VMDonacionMonetaria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(VMDonacionMonetaria);
                }
                ServicioDonacion servicioDonacion = new ServicioDonacion();
                Usuarios usuario = new Usuarios();

                //Valido que los datos ingresados estén bien
               bool montoADonar = servicioDonacion.MontoADonarRecibido(VMDonacionMonetaria);

                if (!montoADonar)
                {
                    ViewBag.mensajeError = "La donación minima es de $100";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return RedirectToAction("DonacionMonetaria");
        }
    }
    }