using JwtUI.Builders.Abstracts;
using JwtUI.Models;

namespace JwtUI.Builders.Concrete
{
    public class SingleRoleStatusBuilder : StatusBuilder
    {
        public override Status GenerateStatus(AppUser activeUser, string roles)
        {
            var status=new Status();

            //Tek bir Rol Belirlenmişse
            if (activeUser.Roles.Contains(roles))
            {
                status.AccessStatus = true;
            }

            return status;
        }
    }
}