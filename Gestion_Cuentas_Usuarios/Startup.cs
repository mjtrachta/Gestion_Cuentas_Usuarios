using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Gestion_Cuentas_Usuarios.Entidades;
using Gestion_Cuentas_Usuarios.Service;
using Gestion_Cuentas_Usuarios.Service.Interface;


namespace Gestion_Cuentas_Usuarios
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configura el DbContext con SQL Server
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configura los servicios de la aplicación
            services.AddScoped<IClienteService, ClienteService>();
            

            // Configuraciones adicionales de servicios...
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configuraciones para entorno de producción
                // Por ejemplo: app.UseExceptionHandler("/Home/Error");
                // app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Habilita el enrutamiento de controladores API
            });
        }
    }
}