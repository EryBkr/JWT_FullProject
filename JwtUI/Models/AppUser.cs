using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JwtUI.Models
{
    public class AppUser
    {
      
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}