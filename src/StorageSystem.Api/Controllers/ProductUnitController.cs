using Microsoft.AspNetCore.Mvc;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.ProductUnit;

namespace StorageSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUnitController : ControllerBase
    {
        private readonly IProductUnitService _productUnitService;

        public ProductUnitController(IProductUnitService productUnitService)
        {
            _productUnitService = productUnitService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductUnitCreateDto model)
        {
            var productUnitId = await _productUnitService.CreateProductUnitAsync(model);

            return Ok(productUnitId);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] ProductUnitUpdateDto model)
        {
            var productUnitId = await _productUnitService.UpdateProductUnitAsync(model);

            return Ok(productUnitId);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _productUnitService.DeleteProductUnitAsync(id);

            return Ok(isDeleted);
        }

        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var isDeleted = await _productUnitService.SoftDeleteProductUnitAsync(id);

            return Ok(isDeleted);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var productUnit = await _productUnitService.GetProductUnitByIdAsync(id);

            return Ok(productUnit);
        }

        [HttpPost("GetAll")]
        public ActionResult GetAll()
        {
            var productUnits = _productUnitService.GetAllProductUnits();

            return Ok(productUnits);
        }
    }
}
