using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAH.API.Database.Entities;

//data annotations para la tabla, nombre de tabla, esquemas (sirve para agrupar) 
//dbo es un esquema usual en sql
[Table("categories", Schema = "dbo")]
public class CategoryEntity : BaseEntity
{
    // Desactivar <Nullable>disable</Nullable> en la carpeta del proyecto para evitar errores de variables tipo string
    // Data Annotations
    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El {0} de la categoria es requerido.")]
    [StringLength(50)]
    [Column("name")]
    public string Name { get; set; }

    [Display(Name = "Descripcion")]
    [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres")]
    [StringLength(250)]
    [Column("description")]
    //Los {} del data annotations son en orden, en este caso Display {0} hace referencia al Display
    //y el {1} hace referencia al MinLenght
    public string Description { get; set; }
    
    //no se coloca List porque el IEnumerable es solo de lectura
    public virtual IEnumerable<PostEntity> Posts { get; set; }
    
    //LOS IENUMERABLE y las ForeingKey se ponen en virtual
}
