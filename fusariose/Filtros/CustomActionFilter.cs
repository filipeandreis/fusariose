using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fusariose.Filtros
{
    public class CustomActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.Result = new ContentResult()
            {
                Content = "Recurso indisponível - header não definido"
            };

            //throw new NotImplementedException();
        }
    }
}
