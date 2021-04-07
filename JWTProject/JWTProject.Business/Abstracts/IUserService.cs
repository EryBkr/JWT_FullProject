using JWTProject.Entities.Concrete;
using JWTProject.Entities.DTO.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Business.Abstracts
{
   public interface IUserService : IGenericService<AppUser>
    {
        Task<AppUser> FindByUserName(string userName);
        Task<bool> CheckPassword(UserLoginDto user);
        Task<List<AppRole>> GetRolesByUserName(string userName);
    }
}
