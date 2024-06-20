using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.Models;

[Table("Enquiry")]
public partial class Enquiry
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column(TypeName = "character varying")]
    public string? Description { get; set; }

    [Column(TypeName = "character varying")]
    public string? Name { get; set; }

    [Column(TypeName = "character varying")]
    public string? Email { get; set; }

    public int? PorpertyId { get; set; }

    public int? AgentId { get; set; }

    [ForeignKey("AgentId")]
    [InverseProperty("Enquiries")]
    public virtual Agent? Agent { get; set; }

    [ForeignKey("PorpertyId")]
    [InverseProperty("Enquiries")]
    public virtual Property? Porperty { get; set; }
}
