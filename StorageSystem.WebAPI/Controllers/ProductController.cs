using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorageSystem.Application.Constracts.Services.Features;
using StorageSystem.Application.Models.Bases;
using OneOf;
using OneOf.Types;
using StorageSystem.Domain.Entities;
using NPOI.SS.Formula.Functions;
using StorageSystem.Application.Models.Product.Ins;

namespace StorageSystem.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductInsDto productDto)
        {
            var result = await _productService.CreateProduct(productDto);
            return result.Match<IActionResult>(
                _ => Ok(result.AsT0),
                BadRequest,
                BadRequest
            );
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Paging paging)
        {
            var result = await _productService.GetAllProducts(paging);
            return result.Match<IActionResult>(
                res => Ok(new
                {
                    res.ProductLists, res.Total
                }),
                BadRequest,
                BadRequest
            );
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> FindProductById(Guid id)
        {
            var result = await _productService.FindProductById(id);
            return result.Match<IActionResult>(
                _ => Ok(result.AsT0),
                BadRequest,
                res => BadRequest(res)
            );
        }

        //[Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductInsDto product)
        {
            var result = await _productService.UpdateProduct(id, product);
            return result.Match<IActionResult>(
                _ => NoContent(),
                BadRequest,
                BadRequest
            );
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProduct(id);
            return result.Match<IActionResult>(
                _ => NoContent(),
                r1 => Ok(result.AsT1),
                r2 => Ok(result.AsT2)
            );
        }
    }
}
