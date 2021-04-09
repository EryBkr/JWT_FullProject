using System.ComponentModel.DataAnnotations;

namespace JwtUI.Models
{
    public class ProductEdit
    {
        [Required(ErrorMessage = "Ürün bilgileriniz eksik")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün Adı Boş olamaz")]
        [Display(Name = "Ürün adı")]
        public string Name { get; set; }
    }
}