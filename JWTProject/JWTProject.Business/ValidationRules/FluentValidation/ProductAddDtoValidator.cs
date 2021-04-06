using FluentValidation;
using JWTProject.Entities.DTO.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Business.ValidationRules.FluentValidation
{
    public class ProductAddDtoValidator : AbstractValidator<AddProductDto>
    {
        public ProductAddDtoValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Ürün adı boş bırakılamaz");
        }
    }
}
