using JwtUI.Builders.Abstracts;
using JwtUI.Models;

namespace JwtUI.Builders.Concrete
{
    public class MultiRoleStatusBuilder : StatusBuilder
    {
        public override Status GenerateStatus(AppUser activeUser, string roles)
        {
            var status=new Status();

            //Example [JwtAuth(Roles='Admin,Member,...')]
            var acceptedRoles = roles.Split(',');
            foreach (var role in acceptedRoles)
            {
                if (activeUser.Roles.Contains(role))
                {
                    status.AccessStatus=true;
                    //Eğer Geçerli Rollerden bir tanesi sağlanmışsa foreech i kırabiliriz
                    break;
                }
            }

            return status;
        }
    }
}