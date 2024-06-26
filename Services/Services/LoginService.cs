using AutoMapper;
using DataAcess.Data;
using DataAcess.DTOs;
using DataAcess.Interfaces;
using DataAcess.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.DTOs;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class LoginService :ILoginService    
    {
        private readonly ILoginRepository _loginrepository;
        private readonly IAgentRepository _agentRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public LoginService(ILoginRepository loginRepository, IAgentRepository agentRepository, IMapper mapper, IConfiguration configuration)
        {
            _loginrepository= loginRepository;
            _agentRepository= agentRepository;
            _mapper= mapper;
            _configuration= configuration;
        }

        public async Task CreateAgent(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var agent= _mapper.Map<Agent>(userDto);
            user.Password = HashPassword(userDto.Password);
            agent.Password=HashPassword(userDto.Password);
            user.RoleId = 2;
            _loginrepository.Add(user);
            _agentRepository.Add(agent);
           
        }
        public async Task CreateUser(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.RoleId = 1;
            user.Password = HashPassword(userDto.Password);
            _loginrepository.Add(user);

        }
        public async Task<AuthResponseDto> LoginUser(LoginUserDto loginUser)
        {
            var user = await _loginrepository.GetByUsername(loginUser.Email);
           
            var agent = await _loginrepository.GetAgentByUsername(loginUser.Email);
           
            if (user != null && VerifyPassword(loginUser.Password, user.Password)|| agent==null)
            {

                var token = GenerateJwtToken(user,agent);
                return new AuthResponseDto
                {
                    Token = token,
                    Expiration = DateTime.UtcNow.AddHours(5),
                    UserId=user.Id,
                    AgentId=agent?.AgentId  


                };
            }
            return null;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInputPassword = HashPassword(password);
            return hashedInputPassword == hashedPassword;
        }
        private string GenerateJwtToken(User user,Agent agent)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role,user.RoleId.ToString()) ,
            new Claim("AgentId",agent.AgentId.ToString()),
            
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(20),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
