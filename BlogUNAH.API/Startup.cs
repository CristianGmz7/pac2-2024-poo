//Ing Pale
using BlogUNAH.API.Services;
using BlogUNAH.API.Services.Interfaces;

namespace BlogUNAH.API;

public class Startup
{
    private IConfiguration Configuration { get; }

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
