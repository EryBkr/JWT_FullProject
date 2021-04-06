using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorCatchController : ControllerBase
    {
        [Route("/Error")] //StartUp da ki url i burada tanımladık
        public IActionResult Error()
        {
            var errorInfo=HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //Log işlemi yapılabilir


            return Problem(detail:"Bir Hata İle Karşılaştık");
        }
    }
}
