/*using Entidades.Views;
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
        ServicioNecesidad servicioNecesidad = new ServicioNecesidad();
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
            //ToDo: Fijarse como subir archivos en el github de la materia. Para tmbn asi no tener q usar javascript para guardar el nombre de la imagen
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                //ToDo: Agregar el idUsuario. Aca esta hardcodeado. Persistir en la bs la necesidad. Y pasarle por parametro el id a Insumos o Monetarias. Inicializar necesidad como estado=0 (cerrado) y luego de agregar insumo/necesidad, verificar q tenga eso agregado pa cambiar el estado
                Necesidades necesidad = servicioNecesidad.buildNecesidad(vmnecesidad, 3);
                if (Enum.GetName(typeof(TipoDonacion), vmnecesidad.TipoDonacion) == "Insumos")
                {
                  
                    return RedirectToAction("Insumos", "Necesidades", necesidad.IdNecesidad); //asi

                }
                else
                {
                    return RedirectToAction("Monetaria", "Necesidades", necesidad);
                }
            }

        }
        //toDo: cambiar necesidades x idNecesidad. Y utilizarlo asi
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
}*/