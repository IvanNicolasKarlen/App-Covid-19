using Entidades.Views;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCovid19.Services;

namespace WebCovid19.Controllers
{
    public class NecesidadesController : Controller
    {
        // GET: Necesidades
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Crear()
        {
            VMNecesidad necesidad = new VMNecesidad();
            return View(necesidad);
        }

        [HttpPost]
        public ActionResult Crear(VMNecesidad vmnecesidad)
        {
            //ToDo: Falta agregar la logica aca

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                ServicioNecesidad servicioNecesidad = new ServicioNecesidad();
                //ToDo: Agregar el idUsuario. Aca esta hardcodeado
                Necesidades necesidad = servicioNecesidad.buildNecesidad(vmnecesidad, 3);
                if (Enum.GetName(typeof(TipoDonacion), vmnecesidad.TipoDonacion) == "Insumos")
                {
                  
                    return RedirectToAction("Insumos", "Necesidades", necesidad);

                }
                else
                {
                    return RedirectToAction("Monetaria", "Necesidades", necesidad);
                }
            }

        }
        //toDo: No entiendo como hacer esto:
        public ActionResult Insumos(Necesidades necesidades)
        {
           
            NecesidadesDonacionesInsumos insumos = new NecesidadesDonacionesInsumos();
            insumos.Necesidades = necesidades;
            return View(insumos);
        }            
        
        [HttpPost]
        public ActionResult Insumos(NecesidadesDonacionesInsumos insumos)
        {
            return View();
        }

        public ActionResult Monetaria(Necesidades necesidades)
        {
            
            NecesidadesDonacionesMonetarias monetaria = new NecesidadesDonacionesMonetarias();
            monetaria.Necesidades = necesidades;
            //monetaria.IdNecesidad = necesidades.IdNecesidad;
            return View(monetaria);
        }

        public ActionResult Detalles()
        {
            Session["url"] = Request["url"];
            if (Session["Email"] as string == "")
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }


    }
}