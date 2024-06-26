using DataAcess.CustomModel;
using DataAcess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.IServices;

namespace RealEstateApi.Controllers
{
    [ApiController]
    [Route("api/RealEstateApi")]
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyInterface;   

        public PropertyController(IPropertyService propertyService)
        {
            _propertyInterface = propertyService;
        }
        [HttpGet(nameof(GetProperties))]

        public async Task<ActionResult<List<PropertyDto>>> GetProperties([FromQuery] Paginationparameters paginationparameters,string searchvalue)
        {
            try
            {
                
                var properties = await _propertyInterface.GetAllPropertyAsync(paginationparameters, searchvalue);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [Authorize(Roles = "2,1")]
        [HttpGet(nameof(GetPropertiesDetails))]
        public async Task<ActionResult<PropertyDto>> GetPropertiesDetails(int propertyid)
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
        [Authorize(Roles = "2")]
        [HttpPost(nameof(CreateProperty))]
        public async Task<ActionResult> CreateProperty(Property property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            try
            {
                await _propertyInterface.AddPropertyAsync(property);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [Authorize(Roles = "2")]
        [HttpPost(nameof(GetPropertyListByAgentId))]
        public async Task<ActionResult<List<PropertyDto>>> GetPropertyListByAgentId(int id, [FromQuery] Paginationparameters paginationparameters)
        {
            if (id == null) throw new ArgumentNullException(nameof(GetPropertyListByAgentId));
            try
            {
                var res = await _propertyInterface.GetPropertyListByAgentId(id, paginationparameters);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [Authorize(Roles = "1")]
        [HttpPost(nameof(AddEnquiry))]
        public async Task<ActionResult> AddEnquiry(Enquiry enquiry)
        {
            await _propertyInterface.AddEnquiry(enquiry);
            return Ok();

        }
        [Authorize(Roles = "2")]
        [HttpPost(nameof(GetEnquiry))]
        public ActionResult GetEnquiry(int id)
        {

            var res = _propertyInterface.GetEnquiry(id);
            return Ok(res);
        }
      
        [HttpGet(nameof(CreateChart))]
        public IActionResult CreateChart()
        {
            var res = _propertyInterface.CreateChart();
            return Ok(res);

        }


    }
}
