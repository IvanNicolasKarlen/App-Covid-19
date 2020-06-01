using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCovid19.Content.Utilities;
using Servicios;
using Entidades.Views;
using Entidades;

namespace WebCovid19.Controllers
{
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
                    return View(VMDonacionMonetaria);
                }
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

        [HttpPost]
        public ActionResult FotoPerfil(VMDonacionMonetaria VMDonacionMonetaria)
        {
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {

                //creo un nombre significativo en este caso apellidonombre pero solo un caracter del nombre, ejemplo BatistutaG
                string nombreSignificativo = VMDonacionMonetaria.NombreSignificativoImagen;
                //Guardar Imagen
                string pathRelativoImagen = ImagenesUtility.Guardar(Request.Files[0], nombreSignificativo);
                VMDonacionMonetaria.Foto = pathRelativoImagen;
            }


            TempData["usuarioCreado"] = true;

            return RedirectToAction("Index");
        }


        /*  [HttpGet]
          public ActionResult VerTotalDeDonacion()
          {
              return View();
          }*/


        public ActionResult VerTotalDeDonacion()
        {
            /*SUMATORIA TOTAL RECAUDADO*/
            int IdDonacionMonetaria = 1;
            decimal Sumatoria = servicioDonacion.TotalRecaudado(IdDonacionMonetaria);
            ViewBag.Sumatoria = Sumatoria;
            decimal Suma = Sumatoria;

            /*PEDIDO DE DONACION*/
            int IdNecesidadDonacionMonetaria = 9;
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
