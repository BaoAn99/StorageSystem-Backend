using Microsoft.AspNetCore.Mvc;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.ProductTypes;
using StorageSystem.Domain.Commons;

namespace StorageSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductTypeCreateDto model)
        {
            var productTypeId = await _productTypeService.CreateProductTypeAsync(model);
            return Ok(productTypeId);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] ProductTypeUpdateDto model)
        {
            var productTypeId = await _productTypeService.UpdateProductTypeAsync(model);
            return Ok(productTypeId);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _productTypeService.DeleteProductTypeAsync(id);
            return Ok(isDeleted);
        }

        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var isDeleted = await _productTypeService.SoftDeleteProductTypeAsync(id);
            return Ok(isDeleted);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var productType = await _productTypeService.GetProductTypeByIdAsync(id);
            return Ok(productType);
        }

        [HttpPost("GetAll")]
        public ActionResult GetAll(QueryParams queryParams)
        {
            var productTypes = _productTypeService.GetAllProductTypes(queryParams);
            return Ok(productTypes);
        }
    }
}
