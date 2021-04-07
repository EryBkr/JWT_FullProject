using FluentValidation;
using JWTProject.Entities.DTO.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Business.ValidationRules.FluentValidation
{
   public class UserLoginDtoValidator:AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(i => i.UserName).NotEmpty().NotNull().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(i => i.Password).NotEmpty().NotNull().WithMessage("Parola boş olamaz");
        }
    }
}
