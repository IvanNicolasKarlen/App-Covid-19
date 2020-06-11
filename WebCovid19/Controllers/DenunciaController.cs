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
    [LoginFilter]
    public class DenunciaController : Controller
    {
        ServicioDenuncia servicioDenuncia;

        public DenunciaController()
        {
            TpDBContext context = new TpDBContext();
            servicioDenuncia = new ServicioDenuncia(context);
        }

        public ActionResult Denunciar(int id)
        {
            ViewBag.motivosDenuncia = servicioDenuncia.ObtenerMotivosDenuncia();
            ViewBag.idNecesidad = id;
            return View();
        }


        [HttpPost]
        public ActionResult Denunciar(Denuncias denuncia)
        {
            int idUsuario = int.Parse(Session["UserId"].ToString());
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.motivosDenuncia = servicioDenuncia.ObtenerMotivosDenuncia();
                    ViewBag.idNecesidad = denuncia.IdNecesidad;
                    return View();
                }

                Denuncias denunciaRegistrada = servicioDenuncia.GuardarDenuncia(denuncia, idUsuario);
                if (denunciaRegistrada == null)
                {
                    ViewBag.mensajeError = "Ha ocurrido un error. Intente nuevamente por favor";
                    return View();
                }
                else
                {
                    ViewBag.mensajeCorrecto = "La denuncia se registró con éxito";
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }
            ViewBag.motivosDenuncia = servicioDenuncia.ObtenerMotivosDenuncia();
            ViewBag.idNecesidad = denuncia.IdNecesidad;
            return View(denuncia);
        }

    }
}