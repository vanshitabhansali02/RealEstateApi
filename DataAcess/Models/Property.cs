using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.Models;

public partial class Property
{
    [Column("ID")]
    public int Id { get; set; }

    [Column(TypeName = "character varying")]
    public string? Possessionstatus { get; set; }

    public bool? IsCommercial { get; set; }

    [Column(TypeName = "character varying")]
    public string? Builder { get; set; }

    [Column(TypeName = "character varying")]
    public string? Approved { get; set; }

    public long? Price { get; set; }

    [Column(TypeName = "character varying")]
    public string? Landmark { get; set; }

    [Column(TypeName = "character varying")]
    public string? Bedroom { get; set; }

    [Column(TypeName = "character varying")]
    public string? Location { get; set; }

    [Column(TypeName = "character varying")]
    public string? Bathroom { get; set; }

    [Column(TypeName = "character varying")]
    public string? Size { get; set; }

    [Key]
    [Column("id")]
    public int Id1 { get; set; }

    [InverseProperty("Property")]
    public virtual ICollection<AgentProperty> AgentProperties { get; set; } = new List<AgentProperty>();

    [InverseProperty("Porperty")]
    public virtual ICollection<Enquiry> Enquiries { get; set; } = new List<Enquiry>();
}
