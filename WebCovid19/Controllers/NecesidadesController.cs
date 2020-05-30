using Entidades;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Servicios;
using WebCovid19.Utilities;
using Entidades.Metadata;
using WebCovid19.Filters;

namespace WebCovid19.Controllers
{

    [LoginFilter]
    public class NecesidadesController : Controller
    {
        ServicioNecesidad servicioNecesidad = new ServicioNecesidad();
        ServicioNecesidadesInsumos servicioInsumo = new ServicioNecesidadesInsumos();
        // GET: Necesidades
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Crear()                     /*****CAMBIOS EN LA VISTA, tipada con NECESIDADESMETADATA****/
        {                                               /***ViewModel Necesidad se puede eliminar***/
            // VMNecesidad necesidad = new VMNecesidad();
            NecesidadesMetadata necesidadesMetadata = new NecesidadesMetadata();
            return View(necesidadesMetadata);
        }

        [HttpPost]
        // public ActionResult Crear(VMNecesidad vmnecesidad)
        public ActionResult Crear(NecesidadesMetadata vmnecesidad)
        {
            //ToDo: Falta agregar la logica aca
            //ToDo: Fijarse como subir archivos en el github de la materia. Para tmbn asi no tener q usar javascript para guardar el nombre de la imagen
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    //TODO: Agregar validacion para confirmar que el archivo es una imagen
                    //creo un nombre significativo en este caso apellidonombre pero solo un caracter del nombre, ejemplo BatistutaG
                    string nombreSignificativo = vmnecesidad.Nombre + " " + Session["Email"];
                    //Guardar Imagen
                    string pathRelativoImagen = ImagenesUtil.Guardar(Request.Files[0], nombreSignificativo);
                    vmnecesidad.Foto = pathRelativoImagen;
                }
                //ToDo: Agregar el idUsuario. Aca esta hardcodeado. Persistir en la bs la necesidad. Y pasarle por parametro el id a Insumos o Monetarias. Inicializar necesidad como estado=0 (cerrado) y luego de agregar insumo/necesidad, verificar q tenga eso agregado pa cambiar el estado

                Necesidades necesidad = servicioNecesidad.buildNecesidad(vmnecesidad, 2); /******CAMBIOS DENTRO, LE PUSE NECESIDADMETADATA*****/
                TempData["idNecesidad"] = necesidad.IdNecesidad;
                if (Enum.GetName(typeof(TipoDonacion), vmnecesidad.TipoDonacion) == "Insumos")
                {
                  
                    return View("Insumos"); //asi

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
            NecesidadesDonacionesInsumos insumos = new NecesidadesDonacionesInsumos();
            string s = TempData["idNecesidad"].ToString();
            int idNecesidad = int.Parse(s);
            insumos.Necesidades = servicioNecesidad.obtenerNecesidadPorId(idNecesidad);
            return View(insumos);
        }            
        
        [HttpPost]
        public ActionResult Insumos(NecesidadesDonacionesInsumos insumos)
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
        //ToDo: ActionResult Monetaria Post. Y crear el servicio y dao correspondiente
       
        public ActionResult Detalles()
        {
            Session["url"] = Request["url"];
            if (Session["Email"] as string == "")
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }


        [HttpPost]
        public ActionResult MisNecesidades(string necesidad)
        {
            ServicioNecesidad servicioNecesidad = new ServicioNecesidad();
            ServicioUsuario servicioUsuario = new ServicioUsuario();
           //ToDo: Usar Session real
            int idSession = 3;
           
            List<Necesidades> necesidadesObtenidas = servicioNecesidad.necesidadesDelUsuario(idSession, necesidad);
            //Mantener el checkbox seleccionado o no, dependiendo lo que haya elegido
            TempData["estadoCheckbox"] = necesidad;
            return View(necesidadesObtenidas);
        }

    }
}