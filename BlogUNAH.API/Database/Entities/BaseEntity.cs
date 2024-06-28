using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAH.API.Database.Entities;
//cosas que estan comunes en todas las tablas se colocaron en BaseEntity
public class BaseEntity
{
    //Estas annotations son las que van en las tablas que se hicieron en el diagrama de objetos
    [Key]       //llave primaria
    [Column("id")]  //nombre de columna que lleva el atributo
    public Guid Id { get; set; }

    [StringLength(100)]     //este StringLenght sirve para tener una cantidad maxima de caracteres
    [Column("created_by")]
    public string CreatedBy { get; set; }

    [Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [StringLength(100)]
    [Column("updated_by")]
    public string UpdatedBy { get; set; }

    [Column("updated_date")]
    public DateTime UpdatedDate { get; set; }

    //campo de logica que se usa por datos, es un campo que queda marcado solo para ocultar
    //public bool IsDeleted { get; set; }
    //el otro es el borrado fisico, que ese si elimina un campo
}
