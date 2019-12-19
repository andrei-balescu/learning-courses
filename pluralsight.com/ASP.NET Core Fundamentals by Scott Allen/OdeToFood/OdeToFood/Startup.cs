using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdeToFood.Data;

namespace OdeToFood
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
            services.AddDbContextPool<OdeToFoodDbContext>(options =>
            {
                string connectionString = Configuration.GetConnectionString("OdeToFoodDb");
                options.UseNpgsql(connectionString);
            });

            // services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();
            services.AddScoped<IRestaurantData, PostgreSqlRestaurantData>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Configures middleware to be run on every request/response in the pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // only concerned with responses
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // forces use of HTTPS
                app.UseHsts();
            }

            app.Use(SayHelloMiddleware);

            app.UseHttpsRedirection();
            // serves files from wwwroot folder
            // can take in a `StaticFileOptions` that configures its behavior
            app.UseStaticFiles();

            // Enables GDPR support: https://docs.microsoft.com/en-us/aspnet/core/security/gdpr?view=aspnetcore-3.1
            // not present by default in .NET Core 3.1
            // app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            // enables websocket communication
            // app.UseSignalR();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                // enables routing to API controllers
                endpoints.MapControllers();
            });
        }

        private RequestDelegate SayHelloMiddleware(RequestDelegate nextMiddlewareInPipeline)
        {
            RequestDelegate helloWorldResponder = async context => 
            {
                const string c_helloWorldEndpoint = "/hello";
                bool isHelloWorldEndpoint = string.Equals(c_helloWorldEndpoint, context.Request.Path, StringComparison.InvariantCultureIgnoreCase);
                if (isHelloWorldEndpoint)
                {
                    await context.Response.WriteAsync("Hello, World!");
                }
                else
                {
                    await nextMiddlewareInPipeline(context);
                }
            };

            return helloWorldResponder;
        }
    }
}
