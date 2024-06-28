using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAH.API.Database.Entities;

[Table("posts", Schema = "dbo")]
public class PostEntity : BaseEntity
{
    [StringLength(100)]
    [Required]
    [Column("title")]
    public string Title { get; set; }

    // TODO: definir la relacion entre usuario y post
    [StringLength(100)]         //tiene que coincidir con el de 
    [Column("author_id")]
    public string AuthorId { get; set; }

    [Column("publication_date")]
    public DateTime PublicationDate { get; set; }

    [Column("category_id")]
    public Guid CategoryId { get; set; }

    //propiedad de navegacion para relacion categoryId y los post, se crea mediante
    //una data anottation llamada ForeingKey, lo mismo hay que hacer en la otra entidad
    //name of convierte una variable a string
    [ForeignKey(nameof(CategoryId))]
    public virtual CategoryEntity Category { get; set; }

    [Column("content")]
    public string Content { get; set; }

    //propiedad de navegacion
    //se le tiene que poner virtual por si en algun motivo se necesita escribir las clases
    public virtual IEnumerable<PostTagEntity> Tags { get; set; }
}
