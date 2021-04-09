using JwtUI.Builders.Concrete;
using JwtUI.Models;

namespace JwtUI.Builders.Abstracts
{
    public abstract class StatusBuilder
    {
        public abstract Status GenerateStatus(AppUser activeUser,string roles);
    }
}