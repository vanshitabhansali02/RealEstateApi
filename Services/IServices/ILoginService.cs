using DataAcess.DTOs;
using DataAcess.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public  interface ILoginService
    {
        Task CreateAgent(CreateUserDto userDto);
        Task CreateUser(CreateUserDto userDto);
        Task<AuthResponseDto> LoginUser(LoginUserDto loginUser);
    }
}
