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
using DAO.Context;

namespace WebCovid19.Controllers
{
    /*[LoginFilter]*/
    public class DonacionMonetariaController : Controller
    {
        ServicioDonacion servicioDonacion;

        public DonacionMonetariaController()
        {
            TpDBContext context = new TpDBContext();
            servicioDonacion = new ServicioDonacion(context);
        }

        [HttpGet]
        public ActionResult DonaMonetaria(int idNecesidad)
        {
            VMDonacionMonetaria vmDonacionMonetaria = new VMDonacionMonetaria();
             vmDonacionMonetaria.IdNecesidad = idNecesidad;
            return View(vmDonacionMonetaria);
        }



        [HttpPost]
        public ActionResult DonaMonetaria(VMDonacionMonetaria VMDonacionMonetaria)
        
        {
            DonacionesMonetarias donacionM = new DonacionesMonetarias();                

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(VMDonacionMonetaria);
                }
                else
                {

                    int idUsuario = int.Parse(Session["UserId"].ToString());
                     donacionM = servicioDonacion.GuardarDonacionM(VMDonacionMonetaria, idUsuario);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            VMComprobantePago vmComprobantePago = new VMComprobantePago();
            vmComprobantePago.IdDonacionMonetaria = donacionM.IdDonacionMonetaria;

            return View("SeleccionComprobanteDePago", vmComprobantePago);
        }

        [HttpGet]
        public ActionResult SeleccionComprobanteDePago()
        {
            return View();
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
                DonacionesMonetarias comprobante = servicioDonacion.Actualizar(VMComprobantePago);

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

        [HttpGet]
        public ActionResult VerTotalDeDonacion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerTotalDeDonacion(int idNecesidadDonacionMonetaria)
        {
            /*SUMATORIA TOTAL RECAUDADO*/
            decimal Sumatoria = servicioDonacion.TotalRecaudado(idNecesidadDonacionMonetaria);
            ViewBag.Sumatoria = Sumatoria;
            decimal Suma = Sumatoria;

            /*PEDIDO DE DONACION*/
            NecesidadesDonacionesMonetarias CantidadSolicitada = servicioDonacion.CantidadSolicitada(idNecesidadDonacionMonetaria);
            ViewBag.CantidadSolicitada = CantidadSolicitada.Dinero;
            decimal CantSolicitada = CantidadSolicitada.Dinero;

            /*TOTAL RESTANTE*/
            decimal calculo = servicioDonacion.CalculoRestaDonacion(Suma, CantSolicitada);
            ViewBag.Restante = calculo;
            return View();
        }
    }
}