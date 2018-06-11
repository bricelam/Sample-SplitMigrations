using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MyApp.Data;

namespace MyApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.MigrationsAssembly("MyApp.Migrations")));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(
                async (context) =>
                {
                    var db = context.RequestServices.GetService<MyDbContext>();

                    await context.Response.WriteAsync("Entities: " + await db.MyEntities.CountAsync());
                });
        }
    }
}
