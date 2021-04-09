using System.ComponentModel.DataAnnotations;

namespace JwtUI.Models
{
    public class ProductAdd
    {
        [Required(ErrorMessage="Boş Geçilemez")]
        [Display(Name="Ürün Adı")]
        public string Name { get; set; }
    }
}