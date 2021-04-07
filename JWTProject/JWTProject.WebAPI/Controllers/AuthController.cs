using AutoMapper;
using JWTProject.Business.Abstracts;
using JWTProject.Entities.Concrete;
using JWTProject.Entities.DTO.UserDtos;
using JWTProject.WebAPI.CustomFilters;
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
                    return Created("", token);
                }
                return BadRequest("Kullanıcı Adı veya şifre hatalı");
            }
        }

        [HttpPost("[action]")]
        [ValidModel] //Model.StateIsValid kontrolü yaptığımız ActionFilter
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            var user = await _userService.FindByUserName(model.UserName);
            if (user != null)
                return BadRequest("Bu Kullanıcıya ait kayıt vardır");
            else
            {
                await _userService.AddAsync(_mapper.Map<AppUser>(model));
                return Created("Başarılıyla kayıt olundu",model);
            }
        }

    }
}
