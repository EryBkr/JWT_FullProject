using System.Threading.Tasks;
using JwtUI.Models;

namespace JwtUI.APIServices.Interfaces
{
    public interface IAuthService
    {
         Task<bool> Login(UserLogin model);
         void LogOut();
    }
}