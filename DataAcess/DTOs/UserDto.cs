using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.DTOs
{
    public class UserDto
    {
        
            [Column(TypeName = "character varying")]
            public string? Email { get; set; }

            [Column(TypeName = "character varying")]
            public string? Password { get; set; }

            [Column(TypeName = "character varying")]
            public string? Username { get; set; }
        [Column(TypeName = "integer")]
        public int? RoleId { get; set; }
        }

    
public class LoginUserDto
{

    [Column(TypeName = "character varying")]
    public string? Email { get; set; }

    [Column(TypeName = "character varying")]
    public string? Password { get; set; }

  
}

    }

