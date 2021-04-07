using AutoMapper;
using JWTProject.Business.Abstracts;
using JWTProject.Business.StringInfos;
using JWTProject.Entities.Concrete;
using JWTProject.Entities.DTO.UserDtos;
using JWTProject.Entities.Token;
using JWTProject.WebAPI.CustomFilters;
using Microsoft.AspNetCore.Authorization;
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
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IJwtService jwtService, IUserService userService,IMapper mapper)
        {
            _jwtService = jwtService;
            _userService = userService;
            _mapper = mapper;
        }


        [HttpPost("[action]")]
        [ValidModel] //Model.StateIsValid kontrolü yaptığımız ActionFilter
        public async Task<IActionResult> SignIn(UserLoginDto model)
        {
            var user = await _userService.FindByUserName(model.UserName);
            if (user == null)
                return BadRequest("Kullanıcı Adı veya şifre hatalı");
            else
            {
                if (await _userService.CheckPassword(model))
                {
                    var roles = await _userService.GetRolesByUserName(user.UserName);
                    var token = _jwtService.GenerateJwt(user, roles);

                    //Token i bir model ile birlikte dönüyorum
                    var tokenModel = new GetTokenModel() {Token=token };
                    return Created("", tokenModel);
                }
                return BadRequest("Kullanıcı Adı veya şifre hatalı");
            }
        }

        [HttpPost("[action]")]
        [ValidModel] //Model.StateIsValid kontrolü yaptığımız ActionFilter
        //FromServices eğer bir controllerda sadece bir action da kullancağımız bir servis varsa DI ı o metodun parametresinde yapıyoruz
        public async Task<IActionResult> Register(UserRegisterDto model,[FromServices] IUserRoleService userRoleService, [FromServices] IRoleService roleService)
        {
            var user = await _userService.FindByUserName(model.UserName);
            if (user != null)
                return BadRequest("Bu Kullanıcıya ait kayıt vardır");
            else
            {
                await _userService.AddAsync(_mapper.Map<AppUser>(model));

                //Kullanıcıya Member Rolünü atadık
                var addedUser = await _userService.FindByUserName(model.UserName);
                var role = await roleService.FindByName(RoleInfos.Member);
                await userRoleService.AddAsync(new AppUserRole { AppRoleId = role.Id, AppUserId = addedUser.Id });

                return Created("Başarılıyla kayıt olundu",model);
            }
        }

        //UI tarafının ihtiyacı olan username , roles gibi dataları geriye dönecek end-point
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetActiveUser()
        {
            //Token dan user name bilgisini handle ediyoruz
            var userName = User.Identity.Name;

            var user = await _userService.FindByUserName(userName);
            var roles = await _userService.GetRolesByUserName(userName);

            //Geriye döneceğim Model i tanımlıyorum
            var activeUser = new ActiveUserDto()
            {
                UserName = user.UserName,
                Roles = roles.Select(i => i.Name).ToList()
            };

            return Ok(activeUser);

        }

    }
}
