using JWTProject.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Entities.Concrete
{
    //Çoka çok ilişki için oluşturuğumuz ek tablo
    public class AppUserRole : IEntity
    {

        public int Id { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }


        public int AppRoleId { get; set; }
        public AppRole AppRole { get; set; }
    }
}
