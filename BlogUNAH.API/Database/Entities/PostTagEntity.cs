﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAH.API.Database.Entities;

[Table("posts_tags", Schema = "dbo")]
public class PostTagEntity : BaseEntity
{
    [Column("post_id")]
    public Guid PostId { get; set; }

    //propiedad de navegacion
    [ForeignKey(nameof(PostId))]
    public virtual PostEntity Post { get; set; }

    [Column("tag_id")]
    public Guid TagId { get; set; }

    //propiedad de navegacion
    [ForeignKey(nameof(TagId))]
    public virtual TagEntity Tag { get; set; }
}
