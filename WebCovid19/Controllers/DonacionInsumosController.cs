using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using DAO.Context;
using Entidades.Views;

namespace WebCovid19.Controllers
{
    public class DonacionInsumosController : Controller
    {

        ServicioDonacionInsumo servicioDonacionInsumo;
        public DonacionInsumosController()
        {
            TpDBContext context = new TpDBContext();
            servicioDonacionInsumo = new ServicioDonacionInsumo(context);
        }

        public ActionResult DonacionInsumos(int idNecesidad)
        {
            NecesidadesDonacionesInsumos NdonacionesI = new NecesidadesDonacionesInsumos();
            NdonacionesI.IdNecesidad = idNecesidad;
            List<NecesidadesDonacionesInsumos> listaNombreInsumos = servicioDonacionInsumo.ListaNombre(NdonacionesI);

            return View("DonacionInsumos", listaNombreInsumos);
        }

        [HttpGet]
        public ActionResult Donar(int idNecesidadDonacionInsumo)
        {
            VMNecesidadesDonacionesInsumos vmNeDoIn = new VMNecesidadesDonacionesInsumos();
            vmNeDoIn.IdNecesidadDonacionInsumo = idNecesidadDonacionInsumo;
            return View(vmNeDoIn);
        }

        [HttpPost]
        public ActionResult Donar(VMNecesidadesDonacionesInsumos ndi)
        {
            DonacionesInsumos donacionI = new DonacionesInsumos();
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    int idUsuario = int.Parse(Session["UserId"].ToString());
                    donacionI= servicioDonacionInsumo.GuardarCantidadDonada(ndi, idUsuario);

                    /*TRAIGO LA CANTIDAD DE INSUMOS DONADOS POR ID y HAGO LA SUMA */
                    int resultado = servicioDonacionInsumo.TraerCantidadDonada(ndi.IdNecesidadDonacionInsumo);
                    ViewBag.CantidadDonadaI = resultado;

                    /*Traigo cantidad solicitada */
                    NecesidadesDonacionesInsumos cantidadInsumosSolicitado = servicioDonacionInsumo.ObtenerCantidadPorId(ndi);
                    ViewBag.CantidadSolicitadaIn = cantidadInsumosSolicitado.Cantidad;
                    int res = cantidadInsumosSolicitado.Cantidad;

                    int restante = servicioDonacionInsumo.InsumoRestante(cantidadInsumosSolicitado.Cantidad, resultado);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }


            return View("GraciasPorDonarInsumos");
        }

        [HttpGet]
        public ActionResult GraciasPorDonarInsumos()
        {
            return View();
        }
    }
}