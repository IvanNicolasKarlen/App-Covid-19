﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace WebCovid19.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registro()
        {
            Usuarios usuario = new Usuarios();
            return View(usuario);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro(Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            

            return View("Index");
        }



        [HttpGet]
        public ActionResult Login()
        {
            Usuarios usuario = new Usuarios();
            return View(usuario);
        }




        [HttpPost]
        public ActionResult Login(Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            return View("Index");
        }

    }
}