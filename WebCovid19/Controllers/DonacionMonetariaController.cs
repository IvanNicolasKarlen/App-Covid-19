using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCovid19.Models.Views;
using WebCovid19.Services;

namespace WebCovid19.Controllers
{
    public class DonacionMonetariaController : Controller
    {
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