using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyConsultaMedica.App_Start
{

    // Acceso o Authorize con Session https://www.c-sharpcorner.com/blogs/applying-authorization-using-session-in-asp-net-mvc
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller  
                return;
            }

            // Check for authorization  
            if (HttpContext.Current.Session["medicoLogueado"] == null)
            {
                filterContext.Result = new RedirectResult("~/Medico/Login");
            }
        }
    }
}