using AutoMapper;
using FluentValidation.AspNetCore;
using JWTProject.Business.DependencyResolvers.MicrosoftIoC;
using JWTProject.Business.StringInfos;
using JWTProject.WebAPI.CustomFilters;
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
            services.AddScoped(typeof(ValidId<>)); //Yapýcý metodunun DI ile Execute edilmesi için ekledik 

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
                opt.RequireHttpsMetadata = false;//HTTP ile çalýþmak durumundayýz bu yüzden SSL zorunluluðunu kaldýrýyoruz

                //Token özellikleri
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = JwtInfo.Issuer, //Kim oluþturdu
                    ValidAudience = JwtInfo.Auidience, //Kim için oluþturuldu
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecurityKey)),
                    ValidateIssuerSigningKey = true,//Key doðrulamasý yapýlsýn mý
                    ValidateLifetime = true,//Token süresi kontrol edilsin mi ?
                    ClockSkew=TimeSpan.Zero //Zaman farký oluþmamasý için ekledik
                };

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTProject.WebAPI v1"));
            }

            //Merkezi hata yakalama Action na yönlendirdik.Beklenmedik bir hata oluþtuðunda bunu genel olarak yakalamak önemlidir
            app.UseExceptionHandler("/Error");

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
