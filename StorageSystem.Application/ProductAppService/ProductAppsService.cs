using AutoMapper;
using StorageSystem.Application.ProductAppService.Dtos;
using StorageSystem.Application.ProductImageAppService;
using StorageSystem.DataAccess.IRepository;
using StorageSystem.Models.Catalog.ProductImages;
using StorageSystem.Models.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.ProductAppService
{
    public class ProductAppsService : IProductAppService
    {
        private readonly IMapper _mapper;
        private readonly Irepository<Product> _productRepository;
        private readonly Irepository<ProductImage> _productImageRepository;

        public ProductAppsService(
            Irepository<Product> productRepository,
            IMapper mapper,
            Irepository<ProductImage> productImageRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productImageRepository = productImageRepository;
        }

        public async Task<List<GetProductForView>> GetAll()
        {
            try
            {
                var listProduct = await _productRepository!.GetAll();
                var query = listProduct.Select(x => new GetProductForView()
                {
                    Product = _mapper.Map<ProductDto>(x)
                }).ToList();

                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetProductForEditOutput> GetProductForEdit(int id)
        {
            var product = await _productRepository.GetById(id);

            var productForEdit = new GetProductForEditOutput
            {
                Name = product.Name,
                Description = product.Description,
            };
            return productForEdit;
        }

        public async Task CreateProduct(CreateProductDto input)
        {
            var product = _mapper.Map<Product>(input);
            await _productRepository.Create(product);
        }

        public async Task UpdateProduct(int id, UpdateProductDto input)
        {
            var product = await _productRepository.GetById(id);
            if (product != null)
            {
                var mapperedProduct = _mapper.Map(input, product);
                await _productRepository.Update(mapperedProduct);
            }
        }

        public async Task Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            await _productRepository.Delete(product);
        }


    }
}
