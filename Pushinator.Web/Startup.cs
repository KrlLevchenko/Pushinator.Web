using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pushinator.Web.AppStart;
using Pushinator.Web.Model;

namespace Pushinator.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(configure => configure.Filters.Add(new AuthorizeFilter()));

            var connectionString = _configuration.GetConnectionString("Db");
            
            services.AddScoped(_ => new Context(connectionString));
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "Client/public"; });

            services.AddMediatR(typeof(Startup).Assembly);
            services.AddValidators(typeof(Startup));
            services.AddErrorHandling(typeof(Startup));

            services.AddMigrator(connectionString);

            services.AddAutoMapper(typeof(Startup));

            services.AddJwt();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseExceptionHandlingMiddleware();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client";

                if (env.IsDevelopment()) spa.UseReactDevelopmentServer("start");
            });
        }
        
        
    }
}