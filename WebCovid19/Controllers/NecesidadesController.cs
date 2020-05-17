using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            Necesidades necesidades = new Necesidades();
            return View(necesidades);
        }

        [HttpPost]
        public ActionResult Crear(Necesidades necesidades)
        {
            //ToDo: Falta agregar la logica aca
            necesidades.FechaCreacion = DateTime.Now;

            return null;
        }
    }
}