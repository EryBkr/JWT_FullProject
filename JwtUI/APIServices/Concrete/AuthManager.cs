using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JwtUI.APIServices.Interfaces;
using JwtUI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace JwtUI.APIServices.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly HttpClient _httpClient;

        public AuthManager(IHttpContextAccessor accessor, HttpClient httpClient)
        {
            _accessor = accessor;
            _httpClient = httpClient;
        }

        public async Task<bool> Login(UserLogin model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44302/api/auth/SignIn", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var token=JsonConvert.DeserializeObject<AccessToken>(await response.Content.ReadAsStringAsync());
                _accessor.HttpContext.Session.SetString("token",token.Token );

                return true;
            }
            else{
                return false;
            }
        }

        public void LogOut()
        {
            _accessor.HttpContext.Session.Remove("token");
        }
    }
}