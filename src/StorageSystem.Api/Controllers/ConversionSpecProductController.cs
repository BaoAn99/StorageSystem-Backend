using Microsoft.AspNetCore.Mvc;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.ConversionSpecs;

namespace StorageSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionSpecProductController : ControllerBase
    {
        private readonly IConversionSpecProductService _conversionSpecProductService;

        public ConversionSpecProductController(IConversionSpecProductService conversionSpecProductService)
        {
            _conversionSpecProductService = conversionSpecProductService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ConversionSpecProductCreateDto model)
        {
            var conversionSpecProductId = await _conversionSpecProductService.CreateConversionSpecProductAsync(model);
            return Ok(conversionSpecProductId);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] ConversionSpecProductUpdateDto model)
        {
            var conversionSpecProductId = await _conversionSpecProductService.UpdateConversionSpecProductAsync(model);
            return Ok(conversionSpecProductId);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _conversionSpecProductService.DeleteConversionSpecProductAsync(id);
            return Ok(isDeleted);
        }

        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var isDeleted = await _conversionSpecProductService.SoftDeleteConversionSpecProductAsync(id);
            return Ok(isDeleted);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var conversionSpecProduct =await _conversionSpecProductService.GetConversionSpecProductByIdAsync(id);
            return Ok(conversionSpecProduct);
        }

        [HttpPost("GetAll")]
        public ActionResult GetAll()
        {
            var conversionSpecProducts = _conversionSpecProductService.GetAllConversionSpecProducts();
            return Ok(conversionSpecProducts);
        }
    }
}
