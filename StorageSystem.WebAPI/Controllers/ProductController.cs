using Microsoft.AspNetCore.Mvc;
using StorageSystem.Application.ProductAppService;
using StorageSystem.Application.ProductAppService.Dtos;
using StorageSystem.Models.Catalog.Products;
using StorageSystem.WebAPI.CommonModel;

namespace StorageSystem.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService _productsService;

        public ProductController(IProductAppService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var productList = await _productsService.GetAll();
            var pagedData = productList.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            var totalRecords = productList.Count();

            return Ok(new PagedResponse<List<GetProductForView>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalRecords));
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateOrUpdateProductDto product)
        {
            List<string> error = new List<string>();
            string message = "Success";
            try
            {
                await _productsService.CreateProduct(product);
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
                message = "Fail";
            }
            return Ok(new PagedResponse<string>(message, error));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] CreateOrUpdateProductDto product)
        {
            List<string> error = new List<string>();
            string message = "Success";
            try
            {
                await _productsService.UpdateProduct(id, product);
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
                message = "Fail";
            }
            return Ok(new PagedResponse<string>(message, error));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            List<string> error = new List<string>();
            string message = "Success";
            try
            {
                await _productsService.Delete(id);
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
                message = "Fail";
            }
            return Ok(new PagedResponse<string>(message, error));
        }
    }
}
