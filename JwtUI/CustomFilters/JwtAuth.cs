using System.Net.Http;
using System.Net.Http.Headers;
using JwtUI.Builders.Concrete;
using JwtUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace JwtUI.CustomFilters
{
    //Token güvenliğini filtrelerle sağlıyoruz
    public class JwtAuth : ActionFilterAttribute
    {
        public JwtAuth(string roles)
        {
            this.Roles = roles;

        }
        public string Roles { get; set; }

        //Action Metodu Çalışmadan Önce çalışacak
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Tokeni tutacak değişkeni tanımladık , out parametresi ile bize iletilecektir.
            string token;

            if (JwtAuthHelper.CheckToken(context, out token))
            {
               //Kullanıcıya ait role ve username bilgilerini http message ile birlikte aldık
                var response=JwtAuthHelper.GetActiveUserHttpMessage(token);

                //İşlem başarılı ise
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Belirlenen rollere uygunluğunu kontrol ediyoruz
                    JwtAuthHelper.CheckUserRole(JwtAuthHelper.GetActiveUser(response), Roles, context);

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) //Yetkisiz bir işlem ise
                {
                    //Token geçersiz olduğu için sildik
                    context.HttpContext.Session.Remove("token");

                    //Token gerekli şartları sağlamıyorsa (expire gibi) login olması için yönlendiriyoruz
                    context.Result = new RedirectToActionResult("SignIn", "Auth", null);
                }
                else
                {
                    var statusCode = response.StatusCode.ToString();

                    //Token geçersiz olduğu için sildik
                    context.HttpContext.Session.Remove("token");

                    //Herhangi bir problem ile karşılaştık bunun için code ile birlikte actiona yönlendiriyoruz
                    context.Result = new RedirectToActionResult("ApiError", "Auth", new { code = statusCode });
                }


            }


        }
    }
}