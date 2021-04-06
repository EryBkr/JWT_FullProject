using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTProject.WebAPI.CustomFilters
{
    //Model.State IsValid kontrolü için Attribute yazıyoruz
    public class ValidModel:ActionFilterAttribute
    {
        //Action Çalışmadan önce çalışacak metot
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Actiona gelen model valid değilse 
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            base.OnActionExecuting(context);
        }
    }
}
