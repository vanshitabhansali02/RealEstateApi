using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.Models;

[Table("User")]
public partial class User
{
    [Column(TypeName = "character varying")]
    public string? Email { get; set; }

    [Column(TypeName = "character varying")]
    public string? Password { get; set; }

    [Column(TypeName = "character varying")]
    public string? Username { get; set; }

    [Key]
    public int Id { get; set; }

    public int? RoleId { get; set; }
}
