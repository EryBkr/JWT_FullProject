using JWTProject.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Entities.DTO.UserDtos
{
    public class UserLoginDto:IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
