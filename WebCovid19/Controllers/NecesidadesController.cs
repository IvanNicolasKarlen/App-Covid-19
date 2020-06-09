using Entidades;
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
        ServicioNecesidadesInsumos servicioInsumo;
        ServicioNecesidadesMonetarias servicioMonetaria;
        ServicioNecesidadValoraciones servicioNecesidadValoraciones;

        public NecesidadesController()
        {
            TpDBContext context = new TpDBContext();
             servicioNecesidad = new ServicioNecesidad(context);
             servicioInsumo = new ServicioNecesidadesInsumos(context);
             servicioMonetaria = new ServicioNecesidadesMonetarias(context);
             servicioNecesidadValoraciones = new ServicioNecesidadValoraciones(context);

        }


        // GET: Necesidades
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Crear()
        { 

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
                TempData["idNecesidad"] = necesidad.IdNecesidad;
                if (Enum.GetName(typeof(TipoDonacion), vmnecesidad.TipoDonacion) == "Insumos")
                {
                    return View("Insumos"); 
                }
                else
                {
                    return RedirectToAction("Monetaria", "Necesidades", necesidad.IdNecesidad);
                }
            }

        }

        [HttpGet]
        public ActionResult Insumos()
        {
            NecesidadesDonacionesInsumosMetadata insumos = new NecesidadesDonacionesInsumosMetadata();
            string s = TempData["idNecesidad"].ToString();
            int idNecesidad = int.Parse(s);
            insumos.Necesidades = servicioNecesidad.obtenerNecesidadPorId(idNecesidad);
            return View(insumos);
        }            

        [HttpPost]
        public ActionResult Insumos(NecesidadesDonacionesInsumosMetadata insumos)
        {
            if (!ModelState.IsValid)
            {
                TempData["idNecesidad"] = insumos.Necesidades.IdNecesidad;
                return View();
            }
            servicioInsumo.GuardarInsumos(insumos);
            return View();
        }

        public ActionResult Monetaria()
        {   
            NecesidadesDonacionesMonetarias monetaria = new NecesidadesDonacionesMonetarias();
            string s = TempData["idNecesidad"].ToString();
            int idNecesidad = int.Parse(s);
            monetaria.Necesidades = servicioNecesidad.obtenerNecesidadPorId(idNecesidad);
            return View(monetaria);
        }

        public ActionResult Monetarias(NecesidadesDonacionesMonetariasMetadata monetarias)
        {
            if (!ModelState.IsValid)
            {
                TempData["idNecesidad"] = monetarias.Necesidades.IdNecesidad;
                return View();
            }
            servicioMonetaria.GuardarMonetarias(monetarias);
            return View();
        }

        [HttpPost]
        public ActionResult MisNecesidades(string necesidad)
        {
            int idSession = int.Parse(Session["UserId"].ToString());
            List<Necesidades> necesidadesObtenidas = servicioNecesidad.TraerNecesidadesDelUsuario(idSession, necesidad);
            
            //Mantener el checkbox seleccionado o no, dependiendo lo que haya elegido
            TempData["estadoCheckbox"] = necesidad;
            

            return View(necesidadesObtenidas);
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
                NecesidadesDonacionesMonetarias necDonacionObtenida = servicioMonetaria.obtenerPorIdNecesidad(idNecesidad);
                vMPublicacion.necesidadesDonacionesMonetarias = necDonacionObtenida;
            }
            else if(necesidadObtenida.TipoDonacion == 2)//Insumos
            {
               NecesidadesDonacionesInsumos insumosObtenidos = servicioInsumo.obtenerPorIdNecesidad(idNecesidad);
                vMPublicacion.necesidadesDonacionesInsumos = insumosObtenidos;
            }
            vMPublicacion.necesidad = necesidadObtenida;
            return View(vMPublicacion);
        }

        public string Buscar()
        {
            string input = Request["buscar"];
            List<Necesidades> necesidadesEncontradas = servicioNecesidad.Buscar(input);
            return input;
        }

        [LoginFilter]
        public ActionResult Home()
        {
            int idSession = int.Parse(Session["UserId"].ToString());
            List<Necesidades> todasLasNecesidades = servicioNecesidad.ListarTodasLasNecesidades();
            return View(todasLasNecesidades);
        }

    }
}