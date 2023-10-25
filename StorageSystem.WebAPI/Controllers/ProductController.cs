using Microsoft.AspNetCore.Mvc;
using StorageSystem.Application.Constracts.Services.Features;
using StorageSystem.Application.Models.Bases;
using StorageSystem.Application.Models.Products.Ins;
using OneOf;
using OneOf.Types;
using StorageSystem.Domain.Entities;

namespace StorageSystem.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _iProduct;
        //private readonly IProductImageAppservice _productImageAppservice;

        public ProductController(IProduct iProduct)
        {
            _iProduct = iProduct;
        }

        //[HttpGet("Products")]
        //public async Task<IActionResult> GetAllProducts([FromQuery] Paging filter)
        //{
        //    var validFilter = new Paging(filter.PageNumber, filter.PageSize);
        //    var productList = await _productsService.GetAll();
        //    var pagedData = productList.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
        //        .Take(validFilter.PageSize)
        //        .ToList();

        //    var totalRecords = productList.Count();

        //    return Ok(new PagedResponse<List<GetProductForView>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalRecords));
        //}

        [HttpPost("Product")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var result = await _iProduct.CreateProduct(product);
            return result.Match<IActionResult>(
                _ => NoContent(),
                BadRequest,
                BadRequest
            );
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] UpdateProductDto product)
        //{
        //    List<string> error = new List<string>();
        //    string message = "Success";
        //    try
        //    {
        //        await _productsService.UpdateProduct(id, product);
        //    }
        //    catch (Exception ex)
        //    {
        //        error.Add(ex.Message);
        //        message = "Fail";
        //    }
        //    return Ok(new PagedResponse<string>(message, error));
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Product>> DeleteProduct(int id)
        //{
        //    List<string> error = new List<string>();
        //    string message = "Success";
        //    try
        //    {
        //        await _productsService.Delete(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        error.Add(ex.Message);
        //        message = "Fail";
        //    }
        //    return Ok(new PagedResponse<string>(message, error));
        //}

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


        //}
    }
}
