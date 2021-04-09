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
    public class JwtAuthHelper
    {
        public static void CheckUserRole(AppUser activeUser, string roles, ActionExecutingContext context)
        {
            if (!string.IsNullOrWhiteSpace(roles))
            {

                Status status = null;
                if (roles.Contains(","))
                {
                    //Example [JwtAuth(Roles='Admin,Member,...')]
                    StatusBuilderDirector director = new StatusBuilderDirector(new MultiRoleStatusBuilder());
                    status = director.GenerateStatus(activeUser, roles);
                }
                else
                {
                    //Tek bir Rol Belirlenmişse
                    StatusBuilderDirector director = new StatusBuilderDirector(new SingleRoleStatusBuilder());
                    status = director.GenerateStatus(activeUser, roles);
                }

                CheckStatus(status, context);

            }
        }

        private static void CheckStatus(Status status, ActionExecutingContext context)
        {
            if (!status.AccessStatus)
            {
                //Token gerekli rolü barındırmıyorsa,başka bir sayfaya yönlendiriyoruz
                context.Result = new RedirectToActionResult("AccessDenied", "Auth", null);
            }
        }

        public static AppUser GetActiveUser(HttpResponseMessage responseMessage)
        {
            var activeUser = JsonConvert.DeserializeObject<AppUser>(responseMessage.Content.ReadAsStringAsync().Result);
            return activeUser;
        }

        public static bool CheckToken(ActionExecutingContext context, out string token)
        {
            token = context.HttpContext.Session.GetString("token");
            if (!string.IsNullOrWhiteSpace(token))
                return true;

            //Token yok ise login olması için yönlendiriyoruz
            context.Result = new RedirectToActionResult("SignIn", "Auth", null);
            return false;
        }

        public static HttpResponseMessage GetActiveUserHttpMessage(string token)
        {
                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //Kullanıcı hakkında role ve user name bilgilerini almak için istek attık.Result dememizin sebebi metodumuz asenkron değil
                var response = httpClient.GetAsync("https://localhost:44302/api/auth/GetActiveUser").Result;

                return response;
        }
    }
}