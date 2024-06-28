//Ing Pale
using BlogUNAH.API.Database;
using BlogUNAH.API.Services;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogUNAH.API;

public class Startup
{
    private IConfiguration Configuration { get; }
    //Esta variable accede al appseseting.Development Json y se pasa en services.AddDbContext

    public Startup( IConfiguration configuration )
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) 
    {
        //Mas o menos aqui se aplican las Data Annotations
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //Add DbContext (comienza configuracion de base de datos)
        services.AddDbContext<BlogUNAHContext>(options => 
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Add custom services
        services.AddTransient<ICategoriesService, CategoriesService>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
