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
            vmNeDoIn.Cantidad = idNecesidadDonacionInsumo;
            return View(vmNeDoIn);

            //NecesidadesDonacionesInsumos necesidadesDonacionesInsumos = new NecesidadesDonacionesInsumos();
            //NecesidadesDonacionesInsumos necesidadesDonaInsumos = servicioDonacionInsumo.BuscarNecesidadesDonacionIPorId(idNecesidadDonacionInsumo);
            //return View("Donar", necesidadesDonaInsumos);
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
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }


            return View();
        }

        [HttpPost]
        public ActionResult GuardarDonacion(NecesidadesDonacionesInsumos necesidadesDonacionesInsumos)
        {
            return View();
        }

        [HttpGet]
        public ActionResult GraciasPorDonarInsumos()
        {
            return View();
        }
    }
}