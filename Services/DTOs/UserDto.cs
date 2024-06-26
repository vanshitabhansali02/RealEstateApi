using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.DTOs
{
    public class CreateUserDto
    {
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Username { get; set; }
    }
    public class LoginUserDto
    {
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

    }
    public class LoggedinUserDto
        {
        public int Roleid { get; set; }
        public int Userid { get; set; }
        public int Agentid { get;set; }
        public string email { get; set; }
        }


}

