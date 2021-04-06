using JWTProject.Business.Abstracts;
using JWTProject.DataAccess.Abstracts;
using JWTProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Business.Concrete
{
   public class UserRoleManager:GenericManager<AppUserRole>,IUserRoleService
    {
        public UserRoleManager(IGenericDal<AppUserRole> genericDal):base(genericDal)
        {

        }
    }
}
