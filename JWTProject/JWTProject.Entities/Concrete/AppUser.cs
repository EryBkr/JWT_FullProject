using JWTProject.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Entities.Concrete
{
    public class AppUser : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        public List<AppUserRole> AppUserRoles { get; set; } //Çoka çok ilişki
    }
}
