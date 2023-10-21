using AutoMapper;
using StorageSystem.Application.ProductAppService.Dtos;
using StorageSystem.Application.ProductImageAppService.Dtos;
using StorageSystem.DataAccess.IRepository;
using StorageSystem.DataAccess.ProductRepository;
using StorageSystem.Models.Catalog.ProductImages;
using StorageSystem.Models.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StorageSystem.Application.ProductImageAppService
{
    public class ProductImageAppService : IProductImageAppservice
    {
        private readonly IMapper _mapper;
        private readonly Irepository<ProductImage> _productImageRepository;

        public ProductImageAppService(Irepository<ProductImage> productImageRepository, IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }

        public async Task CreateProductImage(CreateProductImageDto input)
        {
            var productImage = _mapper.Map<ProductImage>(input);
            await _productImageRepository.Create(productImage);
        }

        public async Task Delete(int id)
        {
            var product = await _productImageRepository.GetById(id);
            await _productImageRepository.Delete(product);
        }

        public async Task<List<GetProductImageForView>> GetAll()
        {
            try
            {
                var listProduct = await _productImageRepository!.GetAll();
                var query = listProduct.Select(x => new GetProductImageForView()
                {
                    ProductImage = _mapper.Map<ProductImageDto>(x)
                }).ToList();

                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetProducImagetByProduct>> GetImageProductByProductId(int productId)
        {
            var productImages = await _productImageRepository.GetAll();
            var listProductImage = productImages?.Where(x => x.ProductId == productId);

            var query = listProductImage.Select(x => new GetProducImagetByProduct()
            {
                Id = x.ProductId,
                ImagePath = x.ImagePath
            }).ToList();

            return query;
        }

        public Task UpdateProductImage(int id, UpdateProductImageDto input)
        {
            throw new NotImplementedException();
        }
    }
}
