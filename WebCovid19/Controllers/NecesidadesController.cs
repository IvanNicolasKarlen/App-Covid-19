﻿using Entidades;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Servicios;
using WebCovid19.Utilities;
using Entidades.Metadata;
using WebCovid19.Filters;
using Entidades.Views;
using Entidades.Enum;
using DAO.Context;

namespace WebCovid19.Controllers
{

    [LoginFilter]
    public class NecesidadesController : Controller
    {
        ServicioNecesidad servicioNecesidad;
        ServicioNecesidadValoraciones servicioNecesidadValoraciones;

        public NecesidadesController()
        {
            TpDBContext context = new TpDBContext();
             servicioNecesidad = new ServicioNecesidad(context);
             servicioNecesidadValoraciones = new ServicioNecesidadValoraciones(context);

        }


        // GET: Necesidades
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Crear()
        {
            int idUsuario = int.Parse(Session["UserId"].ToString());
            if (servicioNecesidad.TraerNecesidadesDelUsuario(idUsuario, "on").Count >= 3)
            {
                ViewBag.Mensaje = "Usted ya alcanzó el límite (3) de necesidades activas.";
                return View("AvisosNecesidad");
            }
            NecesidadesMetadata necesidadesMetadata = new NecesidadesMetadata();
            return View(necesidadesMetadata);
        }

        [HttpPost]
        public ActionResult Crear(NecesidadesMetadata vmnecesidad)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    string nombreSignificativo = vmnecesidad.Nombre + " " + Session["Email"];
                    //Guardar Imagen
                    string pathRelativoImagen = ImagenesUtil.Guardar(Request.Files[0], nombreSignificativo);
                    vmnecesidad.Foto = pathRelativoImagen;
                }
                int idUsuario = int.Parse(Session["UserId"].ToString());
                Necesidades necesidad = servicioNecesidad.buildNecesidad(vmnecesidad, idUsuario); 
                Session["idNecesidad"] = necesidad.IdNecesidad;
                if (Enum.GetName(typeof(TipoDonacion), vmnecesidad.TipoDonacion) == "Insumos")
                {
                    return View("Insumos");
                }
                else
                {
                    return View("Monetaria");
                }
            }
            
        }

        [HttpGet]
        public ActionResult Insumos()
        {
            NecesidadesDonacionesInsumosMetadata insumos = new NecesidadesDonacionesInsumosMetadata();
            return View(insumos);
        }            

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insumos(NecesidadesDonacionesInsumosMetadata insumos)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            int idN = int.Parse(Session["idNecesidad"].ToString());
           insumos.Necesidades = servicioNecesidad.obtenerNecesidadPorId(idN);
           insumos.IdNecesidad = idN;
            servicioNecesidad.AgregarInsumos(insumos);
            return View("Referencias");
        }

        public ActionResult Monetaria()
        {
            NecesidadesDonacionesMonetariasMetadata monetaria = new NecesidadesDonacionesMonetariasMetadata();
            return View(monetaria);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Monetaria(NecesidadesDonacionesMonetariasMetadata monetarias)
        {
            monetarias.Dinero = decimal.Parse(monetarias.Dinero.ToString());
            if (!ModelState.IsValid)
            {
                return View();
            }
            int idN = int.Parse(Session["idNecesidad"].ToString());
            monetarias.Necesidades = servicioNecesidad.obtenerNecesidadPorId(idN);
            monetarias.IdNecesidad = idN;
            servicioNecesidad.AgregarMonetarias(monetarias);
            return View("Referencias");
        }

        public ActionResult Referencias()
        {
            VMReferencias vmReferencia = new VMReferencias();
            return View(vmReferencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Referencias(VMReferencias vmref)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            int idN = int.Parse(Session["idNecesidad"].ToString());
            vmref.Necesidades = servicioNecesidad.obtenerNecesidadPorId(idN);
            vmref.IdNecesidad = idN;
            servicioNecesidad.AgregarReferencias(vmref);
            servicioNecesidad.ActivarNecesidad(idN);
            ViewBag.Mensaje = "La necesidad se creó exitosamente.";
            Session.Remove("idNecesidad");
            return View("AvisosNecesidad");
        }

        [LoginFilter]//toDo: Probar que funcione bien del todo este action.
        public ActionResult DetalleNecesidad(int idNecesidad)
        { 
            int idSession = int.Parse(Session["UserId"].ToString());
            /***************************** Like or Dislike *************************/
            /*Si recibe un Like or dislike desde la vista DetalleNecesidad viene para acá*/
           if(Request.Form["Like"]!=null | (Request.Form["Dislike"] != null))
            {
                string boton = (Request.Form["Like"] != null) ? "Like" : (Request.Form["Dislike"] != null) ? "Dislike" : null;
                LikeOrDislike likeOrDislike = new LikeOrDislike();
                bool estado = likeOrDislike.AgregaLikeOrDislike(idSession, boton, idNecesidad, servicioNecesidadValoraciones);
            }
            
            /**********************************************************************/
            VMPublicacion vMPublicacion = new VMPublicacion();
            Necesidades necesidadObtenida = servicioNecesidad.obtenerNecesidadPorId(idNecesidad);
            List<NecesidadesValoraciones> valoracionesObtenidas = servicioNecesidadValoraciones.obtenerValoracionPorIdNecesidad(idNecesidad);

            if (necesidadObtenida.TipoDonacion == 1)//Dinero
            {
                NecesidadesDonacionesMonetarias necDonacionObtenida = servicioNecesidad.BuscarMonetariasPorIdNecesidad(idNecesidad);
                vMPublicacion.necesidadesDonacionesMonetarias = necDonacionObtenida;
            }
            else if(necesidadObtenida.TipoDonacion == 2)//Insumos
            {
               NecesidadesDonacionesInsumos insumosObtenidos = servicioNecesidad.BuscarInsumosPorIdNecesidad(idNecesidad);
                vMPublicacion.necesidadesDonacionesInsumos = insumosObtenidos;
            }
            vMPublicacion.necesidad = necesidadObtenida;
            return View(vMPublicacion);
        }


        [LoginFilter]
        public ActionResult Home(string necesidad)
        {
            List<Necesidades> todasLasNecesidades;
            int idSession = int.Parse(Session["UserId"].ToString());
            if (!string.IsNullOrEmpty(Request["buscar"]))
            {
                ViewBag.ResultadoBusqueda = true;
                todasLasNecesidades = servicioNecesidad.Buscar(Request["buscar"]);
                if (todasLasNecesidades.Count == 0)
                {
                    ViewBag.ResultadoBusqueda = false;
                }
            }
            else
            {
                todasLasNecesidades = servicioNecesidad.TraerNecesidadesQueNoSonDelUsuario(idSession);
            }
            List<Necesidades> necesidadesDelUser = servicioNecesidad.TraerNecesidadesDelUsuario(idSession, necesidad);
            //Mantener el checkbox seleccionado o no, dependiendo lo que haya elegido
            TempData["estadoCheckbox"] = necesidad;

            ViewBag.necesidadesDelUser = necesidadesDelUser;
            return View(todasLasNecesidades);
        }

    }
}