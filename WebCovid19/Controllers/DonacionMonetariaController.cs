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
using WebCovid19.Utilities;

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
            VMComprobantePago VMComprobantePago = new VMComprobantePago();
            return View(VMComprobantePago);
        }


        [HttpPost]
        public ActionResult SeleccionComprobanteDePago(VMComprobantePago VMComprobantePago)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    {

                        int idUsuario = int.Parse(Session["UserId"].ToString());
                        string nombreSignificativo = idUsuario + " " + Session["Email"];
                        //Guardar Imagen
                        string pathRelativoImagen = ImagenesUtil.Guardar(Request.Files[0], nombreSignificativo);
                        VMComprobantePago.ArchivoTransferencia = pathRelativoImagen;
                    }
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