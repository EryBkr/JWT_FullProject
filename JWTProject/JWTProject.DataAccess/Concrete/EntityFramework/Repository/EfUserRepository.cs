using JWTProject.DataAccess.Abstracts;
using JWTProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfUserRepository : EfGenericRepository<AppUser>, IUserDal
    {
        public async Task<List<AppRole>> GetRolesByUserName(string userName)
        {
            using var context = new JwtContext();

            return await context.AppUsers.Join(context.AppUserRoles, u => u.Id, ur => ur.AppUserId, (user, userRole) => new
            {
                user = user,
                userRole = userRole
            }).Join(context.AppRoles, two => two.userRole.AppRoleId, r => r.Id, (twoTable, role) => new
            {
                twoTable.user,
                userRole = twoTable.userRole,
                role = role
            }).Where(i=>i.user.UserName==userName).Select(i=>new AppRole 
            {
                Id=i.role.Id,
                Name=i.role.Name
            }).ToListAsync();
        }
    }
}
