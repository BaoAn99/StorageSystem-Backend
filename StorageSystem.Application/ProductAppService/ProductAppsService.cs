using AutoMapper;
using StorageSystem.Application.ProductAppService.Dtos;
using StorageSystem.DataAccess.IRepository;
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

        public ProductAppsService(Irepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<GetProductForView>> GetAll()
        {
            try
            {
                var listProduct =  await _productRepository!.GetAll();
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

        public Task<GetProductForEditOutput> GetDefImportFileForEdit(int id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateProduct(CreateOrUpdateProductDto input) {
            var product = _mapper.Map<Product>(input);
            await _productRepository.Create(product);
        }

        public async Task UpdateProduct(int id, CreateOrUpdateProductDto input)
        {
            var product = await _productRepository.GetById(id);
            if (product != null)
            {
                var mapperdProduct = _mapper.Map(input, product);
                await _productRepository.Update(mapperdProduct);
            }
        }

        public async Task Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            await _productRepository.Delete(product);
        }


    }
}
