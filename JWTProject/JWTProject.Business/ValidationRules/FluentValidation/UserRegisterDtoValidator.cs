using FluentValidation;
using JWTProject.Entities.DTO.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Business.ValidationRules.FluentValidation
{
    public class UserRegisterDtoValidator:AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(i => i.FullName).NotEmpty().WithMessage("İsim boş olamaz");
            RuleFor(i => i.Password).NotEmpty().WithMessage("Parola Boş olamaz");
            RuleFor(i => i.UserName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
        }
              
    }
}
