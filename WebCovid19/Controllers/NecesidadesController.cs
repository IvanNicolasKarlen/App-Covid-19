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

        public ActionResult Create()
        {
            Necesidades necesidades = new Necesidades();
            return View(necesidades);
        }
    }
}