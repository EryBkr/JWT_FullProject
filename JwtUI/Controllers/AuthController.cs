using System.Threading.Tasks;
using JwtUI.APIServices.Interfaces;
using JwtUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(UserLogin model)
        {
            if (ModelState.IsValid)
            {
               if(await _authService.Login(model))
               {
                   return RedirectToAction("Index","Home");
               }
               else{
                   ModelState.AddModelError("","Kullanıcı adı veya şifre hatalı");
               }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout(){
            _authService.LogOut();
            return RedirectToAction("SignIn","Auth");
        }
    }
}