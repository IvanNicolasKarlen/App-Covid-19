using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using DAO.Context;

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

        
        public ActionResult Donar(int idNecesidadDonacionInsumo)
        {

            NecesidadesDonacionesInsumos necesidadesDonacionesInsumos = new NecesidadesDonacionesInsumos();
            List <NecesidadesDonacionesInsumos> necesidadesDonaInsumos = servicioDonacionInsumo.BuscarNecesidadesDonacionIPorId(idNecesidadDonacionInsumo);


            return View("Donar", necesidadesDonaInsumos);
        }

        [HttpGet]
        public ActionResult GraciasPorDonarInsumos()
        {
            return View();
        }
    }
}