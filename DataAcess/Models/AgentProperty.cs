using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.Models;

public partial class AgentProperty
{
    [Key]
    public int Id { get; set; }

    public int? AgentId { get; set; }

    public int? PropertyId { get; set; }

    [ForeignKey("AgentId")]
    [InverseProperty("AgentProperties")]
    public virtual Agent? Agent { get; set; }

    [ForeignKey("PropertyId")]
    [InverseProperty("AgentProperties")]
    public virtual Property? Property { get; set; }
}
