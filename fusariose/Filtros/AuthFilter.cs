using fusariose.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace fusariose.Filtros
{
    public class AuthFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var login = filterContext.HttpContext.Session.GetString("login");

            if (String.IsNullOrEmpty(login))
            {
                filterContext.Result = new RedirectResult("/login");
            }
            else if(!String.IsNullOrEmpty(login) && login.Equals("0"))
            {
                filterContext.Result = new RedirectResult("/login");
            }
        }
    }
}
