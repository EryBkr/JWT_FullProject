using JWTProject.Business.Abstracts;
using JWTProject.DataAccess.Abstracts;
using JWTProject.Entities.Concrete;
using JWTProject.Entities.DTO.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Business.Concrete
{
    public class UserManager:GenericManager<AppUser>,IUserService
    {
        private readonly IUserDal userDal;
        public UserManager(IGenericDal<AppUser> genericDal, IUserDal userDal) :base(genericDal)
        {
            this.userDal = userDal;
        }

        public async Task<bool> CheckPassword(UserLoginDto user)
        {
           var appUser=await userDal.GetByFilterAsync(i=>i.UserName==user.UserName);
            if (appUser.Password == user.Password)
                return true;

            return false;
        }

        public async Task<AppUser> FindByUserName(string userName)
        {
            return await userDal.GetByFilterAsync(i => i.UserName == userName);
        }

        public async Task<List<AppRole>> GetRolesByUserName(string userName)
        {
            return await userDal.GetRolesByUserName(userName); 
        }
    }
}
