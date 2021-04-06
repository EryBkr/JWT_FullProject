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
    public class UserManager:GenericManager<AppUser>,IUserService
    {
        public UserManager(IGenericDal<AppUser> genericDal):base(genericDal)
        {
        }

    }
}
