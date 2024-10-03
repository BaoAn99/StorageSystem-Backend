﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public ActionResult Create([FromBody] ProductCreateDto model)
        {
            var productId = _productService.Create(model);

            return Ok(productId);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] ProductUpdateDto model)
        {
            var productId = _productService.Update(model);

            return Ok(productId);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var isDeleted = _productService.Delete(id);

            return Ok(isDeleted);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var product = _productService.GetById(id);

            return Ok(product);
        }

        [HttpPost]
        public ActionResult GetAll()
        {
            var products = _productService.GetAll();

            return Ok(products);
        }
    }
}
