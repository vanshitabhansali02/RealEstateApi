using DataAcess.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace RealEstateApi.Controllers
{
    [ApiController]
    [Route("api/RealEstateApi")]
    public class PropertyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PropertyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        [HttpPost(nameof(CreateProperty))]
        public async Task<IActionResult> CreateProperty([FromBody]Property property)
        {
            try
            {
                if(property == null)
                {
                    return BadRequest("Owner object is null");

                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                 _unitOfWork.Property.CreateProperty(property);
                await _unitOfWork.SaveAsync();  
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
