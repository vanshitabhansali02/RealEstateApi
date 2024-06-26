using AutoMapper;
using DataAcess.CustomModel;
using DataAcess.DTOs;
using DataAcess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.IServices;
using Services.Services;

namespace RealEstateApi.Controllers
{
    [ApiController]
    [Route("api/RealEstateApi")]
    public class LoginController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILoginService _loginService;
        public LoginController(IMapper mapper,  ILoginService userService)
        {
            _mapper = mapper;
            _loginService = userService;

        }
        [HttpPost(nameof(CreateAgent))]
        public async Task<ActionResult> CreateAgent([FromBody] CreateUserDto userdto)
        {
            if (userdto == null)
            {
                return BadRequest("User data is null.");
            }
            try
            {
                await _loginService.CreateAgent(userdto);
                return Ok();
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");

            }

        }

        [HttpPost(nameof(CreateUser))]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDto userdto)
        {
            if (userdto == null)
            {
                return BadRequest("User data is null.");
            }
            try
            {
                await _loginService.CreateUser(userdto);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");

            }

        }
        [HttpPost(nameof(LoginUser))]
        public async Task<ActionResult> LoginUser([FromBody] LoginUserDto userdto)
        {
            if (userdto == null)
            {
                return BadRequest("User data is null.");
            }
            try
            {
                var res= await _loginService.LoginUser(userdto);
                if(res==null)
                {
                    return Unauthorized("Invalid username or password.");

                }
                return Ok(res);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");

            }

        }
       
     

    }
}
