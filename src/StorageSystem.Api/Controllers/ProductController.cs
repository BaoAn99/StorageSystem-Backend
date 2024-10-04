using Microsoft.AspNetCore.Mvc;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Product;

namespace StorageSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto model)
        {
            var productId = await _productService.CreateProductAsync(model);
            return Ok(productId);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDto model)
        {
            var productId = await _productService.UpdateProductAsync(model);
            return Ok(productId);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _productService.DeleteProductAsync(id);
            return Ok(isDeleted);
        }

        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var isDeleted = await _productService.SoftDeleteProductAsync(id);
            return Ok(isDeleted);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPost("GetAll")]
        public ActionResult GetAll()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }
    }
}
