using FluentValidation;
using JWTProject.Entities.DTO.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Business.ValidationRules.FluentValidation
{
    public class ProductUpdateDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(i => i.Id).InclusiveBetween(0,int.MaxValue).WithMessage("Uygunsuz Id");
            RuleFor(i => i.Name).NotEmpty().WithMessage("Ürün adı boş bırakılamaz");
        }
    }
}
