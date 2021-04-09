using System.ComponentModel.DataAnnotations;

namespace JwtUI.Models
{
    public class UserLogin
    {
          [Required(ErrorMessage="Gereklidir")]
          [Display(Name="Kullanıcı Adı")]
        public string UserName { get; set; }

          [Required(ErrorMessage="Parola Gereklidir")]
           [Display(Name="Parola")]
        public string Password { get; set; }
    }
}