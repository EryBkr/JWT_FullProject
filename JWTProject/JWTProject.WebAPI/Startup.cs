using AutoMapper;
using FluentValidation.AspNetCore;
using JWTProject.Business.Abstracts;
using JWTProject.Business.DependencyResolvers.MicrosoftIoC;
using JWTProject.Business.StringInfos;
using JWTProject.WebAPI.CustomFilters;
using JWTProject.WebAPI.Initilaizer;
using JWTProject.WebAPI.Mapping.AutoMapperProfile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllers().AddFluentValidation();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWTProject.WebAPI", Version = "v1" });
            });

            //DI Resolvers
            services.AddDependencies();
            services.AddScoped(typeof(ValidId<>)); //Yap�c� metodunun DI ile Execute edilmesi i�in ekledik 

            //Mapper Config
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());

            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //ADD JWT CONFIG
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => 
            {
                opt.RequireHttpsMetadata = false;//HTTP ile �al��mak durumunday�z bu y�zden SSL zorunlulu�unu kald�r�yoruz

                //Token �zellikleri
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = JwtInfo.Issuer, //Kim olu�turdu
                    ValidAudience = JwtInfo.Auidience, //Kim i�in olu�turuldu
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecurityKey)),
                    ValidateIssuerSigningKey = true,//Key do�rulamas� yap�ls�n m�
                    ValidateLifetime = true,//Token s�resi kontrol edilsin mi ?
                    ClockSkew=TimeSpan.Zero //Zaman fark� olu�mamas� i�in ekledik
                };

            });
        }

        // Servisleri buraya ekleme nedenimiz metodumuz bizden parametre beklemektedir
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUserService userService, IUserRoleService userRoleService, IRoleService roleService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTProject.WebAPI v1"));
            }

            //Merkezi hata yakalama Action na y�nlendirdik.Beklenmedik bir hata olu�tu�unda bunu genel olarak yakalamak �nemlidir
            app.UseExceptionHandler("/Error");

            //Proje aya�a kalkarken temel �yelerin ekliyoruz
            //Wait kullanma nedenimiz metodun kullan�ld��� metot async de�il.
            JwtIdentityInitilaizer.Seed(userService, userRoleService, roleService).Wait();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
