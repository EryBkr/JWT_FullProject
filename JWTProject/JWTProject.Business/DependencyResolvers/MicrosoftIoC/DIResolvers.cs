using FluentValidation;
using JWTProject.Business.Abstracts;
using JWTProject.Business.Concrete;
using JWTProject.Business.ValidationRules.FluentValidation;
using JWTProject.DataAccess.Abstracts;
using JWTProject.DataAccess.Concrete.EntityFramework.Repository;
using JWTProject.Entities.DTO.ProductDtos;
using JWTProject.Entities.DTO.UserDtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Business.DependencyResolvers.MicrosoftIoC
{
    public static class DIResolvers
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            //Tip bağımsız olarak Oluşturuyorum.Hangi Generic yapıyı verirsem ona göre oluşturacaktır
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));

            services.AddScoped<IProductDal, EfProductRepository>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<IUserDal, EfUserRepository>();
            services.AddScoped<IUserService, UserManager>();

            services.AddScoped<IUserRoleDal, EfUserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleManager>();

            services.AddScoped<IRoleDal, EfRoleRepository>();
            services.AddScoped<IRoleService, RoleManager>();

            services.AddScoped<IJwtService, JwtManager>();

            services.AddTransient<IValidator<AddProductDto>, ProductAddDtoValidator>();
            services.AddTransient<IValidator<UpdateProductDto>, ProductUpdateDtoValidator>(); 
            services.AddTransient<IValidator<UserLoginDto>, UserLoginDtoValidator>();
            services.AddTransient<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
        }
    }
}
