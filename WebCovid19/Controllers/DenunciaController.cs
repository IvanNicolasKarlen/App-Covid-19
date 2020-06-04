/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicios;
using Entidades;
using WebCovid19.Filters;

namespace WebCovid19.Controllers
{
    [AdminFilter]
    [LoginFilter]
    public class DenunciaController : Controller
    {
        public ActionResult Denuncia(int id)
        {
            Denuncias denuncia = new Denuncias();
            ServicioNecesidad servicioNecesidad = new ServicioNecesidad();

            //Obtener los motivos de las denuncias para el select
            //List<MotivoDenuncia> motivoDenuncias = servicioMotivoDenuncia.obtenerMotivos();

            Necesidades necesidadDenunciada = servicioNecesidad.obtenerNecesidadPorId(id);
            ViewBag.titulo = necesidadDenunciada.Nombre;
            ViewBag.idNecesidad = id;
            return View();
        }


        [HttpPost]
        public ActionResult Denuncia(Denuncias denuncia)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                ServicioDenuncia servicioDenuncia = new ServicioDenuncia();

                bool denunciaRegistrada = servicioDenuncia.guardarDenuncia(denuncia);

                if (!denunciaRegistrada)
                {
                    ViewBag.mensajeError = "Ha ocurrido un error. Intente nuevamente por favor";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return View(denuncia);
        }

    }
}*/