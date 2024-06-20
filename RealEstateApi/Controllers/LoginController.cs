using AutoMapper;
using DataAcess.DTOs;
using DataAcess.Models;
using Interfaces.IInterfaces;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interface;

namespace RealEstateApi.Controllers
{
    [ApiController]
    [Route("api/RealEstateApi")]
    public class LoginController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILoginInterface<User> _logininterface;
        private readonly IPropertyInterface _propertyInterface;

        public LoginController(IMapper mapper, IPropertyInterface propertyInterface,ILoginInterface<User> loginInterface)
        {
            _mapper = mapper;
            _logininterface = loginInterface;
            _propertyInterface = propertyInterface;


        }
        [HttpPost(nameof(AddAgent))]
        public async Task<ActionResult> AddAgent([FromBody] UserDto user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }
            try
            { 
                    var userdata = _mapper.Map<User>(user);
                   await _logininterface.AddAsync(userdata);
                    return Ok();
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");

            }

        }
        //[HttpPost(nameof(AddAgent))]
        //public async Task<ActionResult> LoginUser([FromBody] LoginUserDto user)
        //{
        //    if (user == null)
        //    {
        //        return BadRequest("User data is null.");
        //    }
        //    try
        //    {
        //        var authenticatedUser = await _userService.AuthenticateAsync(user.Email, user.Password);

        //        if (authenticatedUser == null)
        //        {
        //            // If authentication fails
        //            return Unauthorized("Invalid username or password.");
        //        }

        //        // If authentication succeeds
        //        return Ok("Login successful");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        //    }


        //}
        [HttpGet(nameof(GetProperties))]
        public async Task<ActionResult<List<PropertyDto>>> GetProperties()
        {
            try
            {
                var properties = await _propertyInterface.GetAllPropertyAsync();
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpGet(nameof(GetPropertiesDetails))]
        public async Task<ActionResult<Property>> GetPropertiesDetails(int propertyid)
        {
            try
            {
                var properties = await _propertyInterface.GetAllPropertyDetailsAsync(propertyid);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpPost(nameof(CreateProperty))]
        public async Task<ActionResult> CreateProperty(Property property)
        {
            if(property==null) throw new ArgumentNullException(nameof(property));
            try
            {
               await _propertyInterface.AddPropertyAsync(property);
                return Ok();
            }
            catch(Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpPost(nameof(GetPropertyListByAgentId))]
        public async Task<ActionResult<PropertyDto>> GetPropertyListByAgentId(int id)
        {
            if (id == null) throw new ArgumentNullException(nameof(GetPropertyListByAgentId));
            try
            {
                var res= await _propertyInterface.GetPropertyListByAgentId(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpPost(nameof(AddEnquiry))]
        public async Task<ActionResult> AddEnquiry(Enquiry enquiry)
        {
           await _propertyInterface.AddEnquiry(enquiry);
            return Ok();

        }

        [HttpPost(nameof(GetEnquiry))]
        public ActionResult GetEnquiry(int id)
        {

            var res=_propertyInterface.GetEnquiry(id);
            return Ok(res);
        }


    }
}
