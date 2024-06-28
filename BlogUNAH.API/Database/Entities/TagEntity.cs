using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAH.API.Database.Entities;

[Table("tags", Schema = "dbo")]
public class TagEntity : BaseEntity
{ 
    //EN LOS DTOS ES NCESARIOS PONER LOS MENSAJES en las Entidades no
    [StringLength(20)]
    [Required]
    [Column("name")]
    public string Name { get; set; }
    [StringLength(250)]
    //la data annotation MinLenght solo va en los dtos
    [Column("description")]
    public string Description { get; set; }

    //los enumerables no van a la base de datos
    public virtual IEnumerable<PostTagEntity> Post { get; set; }
}
