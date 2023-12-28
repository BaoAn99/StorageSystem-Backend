using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorageSystem.Application.Constracts.Services.Features;
using StorageSystem.Application.Models.Bases;
using StorageSystem.Application.Models.Products.Ins;
using OneOf;
using OneOf.Types;
using StorageSystem.Domain.Entities;
using NPOI.SS.Formula.Functions;

namespace StorageSystem.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _iProduct;

        public ProductController(IProduct iProduct)
        {
            _iProduct = iProduct;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var result = await _iProduct.CreateProduct(product);
            return result.Match<IActionResult>(
                _ => NoContent(),
                BadRequest,
                BadRequest
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] Product product)
        {
            var result = await _iProduct.UpdateProduct(productId, product);
            return result.Match<IActionResult>(
                _ => NoContent(),
                BadRequest,
                BadRequest
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var result = await _iProduct.DeleteProduct(productId);
            return result.Match<IActionResult>(
                _ => NoContent(),
                BadRequest,
                BadRequest
            );
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllProducts([FromQuery] Paging filter)
        {
            //var validFilter = new Paging(filter.PageNumber, filter.PageSize);
            //var productList = await _productsService.GetAll();
            //var pagedData = productList.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            //    .Take(validFilter.PageSize)
            //    .ToList();

            //var totalRecords = productList.Count();

            //return Ok(new PagedResponse<List<GetProductForView>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalRecords));
            var result = await _iProduct.GetAllProducts(filter);
            return result.Match<IActionResult>(
                _ => Ok(result.AsT0),
                _ => Ok(result.AsT1),
                BadRequest,
                BadRequest
            );
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProductForEdit(int id)
        //{
        //    GetProductForEditOutput product = new GetProductForEditOutput();
        //    List<string> error = new List<string>();
        //    string message = "Success";

        //    try
        //    {
        //        product = await _productsService.GetProductForEdit(id);

        //        var productImages = await _productImageAppservice.GetImageProductByProductId(id);

        //        product.ProductImages = productImages;
        //    }
        //    catch (Exception ex)
        //    {
        //        error.Add(ex.Message);
        //        message = "Fail";
        //    }

        //    return Ok(new PagedResponse<GetProductForEditOutput>(product, message, error));
    }
}
