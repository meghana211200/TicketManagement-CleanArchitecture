using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entites;

public class SupportEngineer
{
    [Key]
    public int se_id { get; set; }

    public int se_user_id { get; set; }

    public bool isAvailable { get; set; }

    [ForeignKey("se_user_id")]
    public virtual User user { get; set; }
}


