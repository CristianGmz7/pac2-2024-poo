using BlogUNAH.API.Database.Entities;
using Microsoft.EntityFrameworkCore;

//NO CAMBIAR NOMBRES DE CAMPOS (COMO TABLAS O COLUMNAS EN SQL MANAGEGMENT)
//TODO SE DEBE HACER DESDE VISUAL STUDIO

//AGREGAR ConnectionsString en appsettings.Devopment.json
//    "DefaultConnection": "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"
//se le añadieron dos ultimas dos lineas mas
//en el appssettings se ignora con .gitignore y lo que se puso en appsettings.Devopment.json
//es recomendable copiarlos a appsettings.json pero sin los valores reales

namespace BlogUNAH.API.Database;
public class BlogUNAHContext : DbContext
{
    //options son las configuraciones que se tiene con la base de datos
    public BlogUNAHContext(DbContextOptions options) : base(options)
    {
    }
    //DbSet trabaja con objetos genericos, DbSet representa una tabla
    public DbSet<CategoryEntity> Categories { get; set; }   //colocar nombres en plurales 
                                                            //cuando se trabaja con tabla

    public DbSet<TagEntity> Tags { get; set; }

    public DbSet<PostEntity> Posts { get; set; }

    public DbSet<PostTagEntity> PostsTags { get; set; }
}
