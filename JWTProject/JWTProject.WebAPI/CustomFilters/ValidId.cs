using JWTProject.Business.Abstracts;
using JWTProject.Entities.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTProject.WebAPI.CustomFilters
{
    //Kullanıcın Get ve Delete için göndermiş olduğu Id valid mi kontrolü
    //Generic olarak kullanabilmek için Interface i implement ettik
    public class ValidId<TEntity>:IActionFilter where TEntity:class,IEntity,new() 
    {
        private readonly IGenericService<TEntity> _genericService;

        public ValidId(IGenericService<TEntity> genericService) //Bunu Attribute parametresi ile göndereceğiz
        {
            _genericService = genericService;
        }


        public void OnActionExecuted(ActionExecutedContext context){}



        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Parametre ismi id olan değeri yakaladık
            var id=context.ActionArguments.Where(i => i.Key == "id").FirstOrDefault().Value;
            var checkedId = (int)id; //object olan id yi int yaptık

            //Result ekleme sebebimiz metodumuz asenkron değil
            var entity=_genericService.GetByIdAsync(checkedId).Result;

            //DB de kayıt yok ise
            if (entity==null)
            {
                context.Result = new NotFoundObjectResult($"{checkedId} id li nesne bulunamadı");
            }
        }
    }
}
