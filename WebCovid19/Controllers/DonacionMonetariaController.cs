using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCovid19.Content.Utilities;
using Servicios;
using Entidades.Views;
using Entidades;
using WebCovid19.Filters;


namespace WebCovid19.Controllers
{
    /*[LoginFilter]*/
    public class DonacionMonetariaController : Controller
    {
        ServicioDonacion servicioDonacion = new ServicioDonacion();


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
                    return View();
                }
                else
                {

                    int idUsuario = int.Parse(Session["UserId"].ToString());
                    DonacionesMonetarias donacionM = servicioDonacion.GuardarDonacionM(VMDonacionMonetaria, idUsuario);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return RedirectToAction("SeleccionComprobanteDePago");
        }

        [HttpGet]
        public ActionResult SeleccionComprobanteDePago()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SeleccionComprobanteDePago(DonacionesMonetarias donacionesMonetarias)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(donacionesMonetarias);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return RedirectToAction("GraciasPorDonarMonetariamente");
        }


        [HttpGet]
        public ActionResult GraciasPorDonarMonetariamente()
        {
            return View();
        }

        public ActionResult VerTotalDeDonacion()
        {
            /*SUMATORIA TOTAL RECAUDADO*/
            int IdNeceDonacionMonetaria = 5;
            decimal Sumatoria = servicioDonacion.TotalRecaudado(IdNeceDonacionMonetaria);
            ViewBag.Sumatoria = Sumatoria;
            decimal Suma = Sumatoria;

            /*PEDIDO DE DONACION*/
            int IdNecesidadDonacionMonetaria = 5;
            NecesidadesDonacionesMonetarias CantidadSolicitada = servicioDonacion.CantidadSolicitada(IdNecesidadDonacionMonetaria);
            ViewBag.CantidadSolicitada = CantidadSolicitada.Dinero;
            decimal CantSolicitada = CantidadSolicitada.Dinero;

            /*TOTAL RESTANTE*/
            decimal calculo = servicioDonacion.CalculoRestaDonacion(Suma, CantSolicitada);
            ViewBag.Restante = calculo;
            return View();
        }
    }
}