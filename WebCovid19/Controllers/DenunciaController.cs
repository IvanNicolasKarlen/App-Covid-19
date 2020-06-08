using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicios;
using Entidades;
using WebCovid19.Filters;
using DAO.Context;

namespace WebCovid19.Controllers
{
    [AdminFilter]
    [LoginFilter]
    public class DenunciaController : Controller
    {
        ServicioNecesidad servicioNecesidad;
        ServicioDenuncia servicioDenuncia;

        public DenunciaController()
        {
            TpDBContext context = new TpDBContext();
            servicioNecesidad = new ServicioNecesidad(context);
            servicioDenuncia = new ServicioDenuncia(context);
        }

        public ActionResult Denuncia(int id)
        {
            Denuncias denuncia = new Denuncias();
          

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
}