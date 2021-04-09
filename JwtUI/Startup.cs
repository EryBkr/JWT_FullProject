using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtUI.APIServices.Concrete;
using JwtUI.APIServices.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JwtUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            //dotnet new web -n JwtUI komutu ile NetCore MVC projemizi VsCode ile oluşturduk
            //MVC için servis ekledik
            //libman kullanımı için ilk başta yüklenmesi gerekiyordu
            //libman install twitter-bootstrap --files css/bootstrap.min.css --files js/bootstrap.min.js bootstrap için kullanım örneği
            services.AddControllersWithViews();

            //Token i session a controller dışında ki bir classtan atayabilmek için ekledik
            services.AddHttpContextAccessor();
            services.AddSession();

            //Hem DI ile interface i doldurduk hemde HttpClient nesnemizi ayrı olarak belirtmemize gerek kalmadı
            services.AddHttpClient<IAuthService,AuthManager>();
            services.AddHttpClient<IProductService,ProductManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSession();

            //wwwroot kullanımı için ekledik
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                //Home/Index default route tanımı yaptık
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
