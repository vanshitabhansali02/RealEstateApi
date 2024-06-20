using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.Models;

[Table("Agent")]
public partial class Agent
{
    [Key]
    public int AgentId { get; set; }

    [Column(TypeName = "character varying")]
    public string? AgentName { get; set; }

    [Column(TypeName = "character varying")]
    public string? Email { get; set; }

    [Column(TypeName = "character varying")]
    public string? Password { get; set; }

    [InverseProperty("Agent")]
    public virtual ICollection<AgentProperty> AgentProperties { get; set; } = new List<AgentProperty>();

    [InverseProperty("Agent")]
    public virtual ICollection<Enquiry> Enquiries { get; set; } = new List<Enquiry>();
}
