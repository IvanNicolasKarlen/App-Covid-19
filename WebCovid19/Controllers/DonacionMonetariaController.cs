﻿using DAO.Context;
using Entidades;
using Entidades.Views;
using Servicios;
using System;
using System.Web.Mvc;
using WebCovid19.Filters;
using WebCovid19.Utilities;

namespace WebCovid19.Controllers
{
    
    public class DonacionMonetariaController : Controller
    {
        ServicioDonacion servicioDonacion;
        ServicioNecesidad servicioNecesidad;

        public DonacionMonetariaController()
        {
            TpDBContext context = new TpDBContext();
            servicioDonacion = new ServicioDonacion(context);
            servicioNecesidad = new ServicioNecesidad(context);
        }

        [LoginFilter]
        //Muestra la lista: CBU, MONTO SOLICITADO Y MONTO RESTANTE.
        public ActionResult DetalleDeDonacion(int idNecesidad)
        {
            Necesidades necesidades = servicioNecesidad.obtenerNecesidadPorId(idNecesidad);
            return View(necesidades);
        }

        [LoginFilter]
        //Pasa el idNecesidadDonacionMonetaria por el boton donar. 
        //DonaMonetaria ingreso el monto a donar
        [HttpGet]
        public ActionResult DonaMonetaria(int idNecesidadDonacionMonetaria)
        {
            VMDonacionMonetaria vmDonacionMonetaria = new VMDonacionMonetaria();
            vmDonacionMonetaria.IdNecesidadDonacionMonetaria = idNecesidadDonacionMonetaria;

            return View(vmDonacionMonetaria);
        }


        [LoginFilter]
        [HttpPost]
        public ActionResult DonaMonetaria(VMDonacionMonetaria DMonetarias)

        {
            DonacionesMonetarias donacionM = new DonacionesMonetarias();

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(DMonetarias);
                }
                else
                {

                    int idUsuario = int.Parse(Session["UserId"].ToString());
                    donacionM = servicioDonacion.GuardarDonacionM(DMonetarias, idUsuario);
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

        [LoginFilter]
        [HttpGet]
        public ActionResult SeleccionComprobanteDePago()
        {
            return View();
        }

        [LoginFilter]
        [HttpPost]
        public ActionResult SeleccionComprobanteDePago(VMComprobantePago donacionesM)
        {
            DonacionesMonetarias comprobante = new DonacionesMonetarias();
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
                        donacionesM.ArchivoTransferencia = pathRelativoImagen;
                    }
                }
                comprobante = servicioDonacion.Actualizar(donacionesM);
                TempData["Mensaje"] = "Gracias por su donación"; //Creo el TempData son el mensaje. Este TempData lo uso en la vista.
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return RedirectToAction("DetalleDeDonacion", new { comprobante.NecesidadesDonacionesMonetarias.IdNecesidad });
        }

        [LoginFilter]
        [HttpGet]
        public ActionResult GraciasPorDonarMonetariamente()
        {
            return View();
        }

        [LoginFilter]
        [HttpGet]
        public ActionResult VerTotalDeDonacion()
        {
            return View();
        }

        [LoginFilter]
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