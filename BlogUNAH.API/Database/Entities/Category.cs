using System.ComponentModel.DataAnnotations;

namespace BlogUNAH.API.Database.Entities;

public class Category
{
    // Desactivar <Nullable>disable</Nullable> en la carpeta del proyecto para evitar errores de variables tipo string
    public Guid Id { get; set; }

    // Data Annotations
    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El {0} de la categoria es requerido.")]
    public string Name { get; set; }

    [Display(Name = "Descripcion")]
    [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres")]
    //Los {} del data annotations son en orden, en este caso Display {0} hace referencia al Display
    //y el {1} hace referencia al MinLenght
    public string Description { get; set; }
}
