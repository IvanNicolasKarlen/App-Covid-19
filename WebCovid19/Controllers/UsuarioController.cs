using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCovid19.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult IndexLogueado()
        {
            if (Session["Email"] as string == "")
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }


        public ActionResult Salir()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("IndexLogueado");
        }


        public ActionResult Perfil()
        {
            Session["url"] = Request["url"];
            if (Session["Email"] as string == "") 
            {
                return RedirectToAction("Login","Home");
            }
            return View();
        }

       
    }
}