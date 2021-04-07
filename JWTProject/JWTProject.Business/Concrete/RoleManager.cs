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
   public class RoleManager:GenericManager<AppRole>,IRoleService
    {
        private readonly IGenericDal<AppRole> _genericDal; //Burada kullanabilmek için field oluşturdum.IRoleDal da olabilirdi farketmez
        public RoleManager(IGenericDal<AppRole> genericDal):base(genericDal)
        {
            _genericDal = genericDal;
        }

        public async Task<AppRole> FindByName(string roleName)
        {
            return await _genericDal.GetByFilterAsync(i => i.Name == roleName);
        }
    }
}
