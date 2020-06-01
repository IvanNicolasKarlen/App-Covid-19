using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebCovid19.Filters
{
    public class ValidarPeticionFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {         
            string action = HttpContext.Current.Session["action"].ToString();
            string controller = HttpContext.Current.Session["controller"].ToString();

            if (action != "")
            {
                filterContext.Result = new RedirectToRouteResult
                    (
                        new RouteValueDictionary
                        {
                            {"Controller", controller },
                            {"Action", action },

                        }
                    );
            }

        }
    }
}