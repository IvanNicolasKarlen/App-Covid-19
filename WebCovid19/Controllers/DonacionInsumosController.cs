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

        public ActionResult Donar(VMNecesidadesDonacionesInsumos ndi)
        {
            DonacionesInsumos donacionI = new DonacionesInsumos();
            NecesidadesDonacionesInsumos nec = new NecesidadesDonacionesInsumos();

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    int idUsuario = int.Parse(Session["UserId"].ToString());
                    donacionI = servicioDonacionInsumo.GuardarCantidadDonada(ndi, idUsuario);

                    //*****Obtener NecesidadDonacionInsumos por medio del id recibido por parametros*****
                    nec = servicioDonacionInsumo.ObtenerNecesidadDonacionInsumosPorId(ndi.IdNecesidadDonacionInsumo);
                    TempData["Mensaje"] = "Gracias por su donación"; //Creo el TempData son el mensaje. Este TempData lo uso en la vista.

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }
            return RedirectToAction("DonacionInsumos", new { nec.IdNecesidad }); /*Aca le paso nec.IdNecesidad porque DonacionInsumos espera un Id. Si no se 
                                                                                                   lo paso, me va a tirar error 404*/

        }
    }
}