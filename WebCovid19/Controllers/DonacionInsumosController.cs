using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using WebCovid19.Models.Views;
using WebCovid19.Services;


namespace WebCovid19.Controllers
{
    public class DonacionInsumosController : Controller
    {
        // GET: DonacionInsumos

        [HttpGet]
        public ActionResult DonacionInsumos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DonacionInsumos(DonacionesInsumos DonacionesInsumos)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(DonacionesInsumos);
                }

                ServicioDonacionInsumo servicioDonacionInsumo = new ServicioDonacionInsumo();

                //Valido que los datos ingresados estén bien
                bool cantidadIngresada = servicioDonacionInsumo.CantidadMinimaDeInsumo(DonacionesInsumos);
                if (!cantidadIngresada)
                {
                    ViewBag.mensajeError = "Debe ingresar al menos un insumo";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return RedirectToAction("GraciasPorDonarInsumos");
        }

        [HttpGet]
        public ActionResult GraciasPorDonarInsumos()
        {
            return View();
        }
    }
}