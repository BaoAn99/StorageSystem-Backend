﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductInsDto productDto)
        {
            var res = await _productService.CreateProduct(productDto);
            return Ok("ok");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Paging paging)
        {
            var result = await _productService.GetAllProducts(paging);
            return result.Match<IActionResult>(
                _ => Ok(result.AsT0),
                BadRequest,
                BadRequest
            );
        }

        [Authorize]
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

        //[HttpGet()]
        //public async Task<IActionResult> GetAllProducts([FromQuery] Paging filter)
        //{
        //    //var validFilter = new Paging(filter.PageNumber, filter.PageSize);
        //    //var productList = await _productsService.GetAll();
        //    //var pagedData = productList.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
        //    //    .Take(validFilter.PageSize)
        //    //    .ToList();

        //    //var totalRecords = productList.Count();

        //    //return Ok(new PagedResponse<List<GetProductForView>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalRecords));
        //    var result = await _iProduct.GetAllProducts(filter);
        //    return result.Match<IActionResult>(
        //        _ => Ok(result.AsT0),
        //        _ => Ok(result.AsT1),
        //        BadRequest,
        //        BadRequest
        //    );
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
    }
}
