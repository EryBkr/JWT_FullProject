using JWTProject.Business.Abstracts;
using JWTProject.Business.StringInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTProject.WebAPI.Initilaizer
{
    public static class JwtIdentityInitilaizer
    {
        //Bu metot middleware e eklenecek.Proje ayağa kalktığında gerekli configürasyonları sağlayacak
        public static async Task Seed(IUserService userService,IUserRoleService userRoleService,IRoleService roleService)
        {

            //Gerekli roller yoksa ekleyelim
           var adminRole=await roleService.FindByName(RoleInfos.Admin);
            if (adminRole==null)
            {
                await roleService.AddAsync(new Entities.Concrete.AppRole { Name= RoleInfos.Admin });
            }


            var memberRole = await roleService.FindByName(RoleInfos.Member);
            if (memberRole == null)
            {
                await roleService.AddAsync(new Entities.Concrete.AppRole { Name = RoleInfos.Member });
            }


            var adminUser = await userService.FindByUserName("Black");
            if (adminUser==null)
            {
                await userService.AddAsync(new Entities.Concrete.AppUser 
                {
                    UserName="Black",
                    Password="1"
                });

                //Database e eklenmiş kullanıcıyı ve role eriştrik.Id değerleriyle birlikte aldık
                var addedAdminRole = await roleService.FindByName(RoleInfos.Admin);
                var addedUser = await userService.FindByUserName("Black");

                //Rol ve kullanıcı ilişkisini sağladık
                await userRoleService.AddAsync(new Entities.Concrete.AppUserRole
                {
                    AppUserId = addedUser.Id,
                    AppRoleId = addedAdminRole.Id
                });
            }

           

        }
    }
}
