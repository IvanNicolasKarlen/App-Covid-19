using Entidades;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebCovid19.Controllers;

namespace WebCovid19.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                Usuarios usuarioObtenido = (Usuarios)HttpContext.Current.Session["IdUsuario"];

                if (usuarioObtenido == null)
                {

                    if (filterContext.Controller is HomeController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Home/Login");
                    }
                }
            }
            catch (Exception)
            {

                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            Log("OnActionExecuting", filterContext.RouteData);
        }



        /*public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
        }*/

        //Obtiene Controller/Action  a donde quise ir
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }
        //Obtiene Controller/Action donde me esta redirigiendo en este mismo momento despues del OnResultExecuted
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting ", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0}- controller:{1} action:{2}", methodName,
                                                                        controllerName,
                                                                        actionName);
            Debug.WriteLine(message);
        }
    }


}
