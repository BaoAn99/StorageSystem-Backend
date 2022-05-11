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

        public IEnumerable<GetProductForView> GetAll()
        {
            try
            {
                var listProduct =  _productRepository!.GetAll().ToList();
                var query = listProduct.Select(x => new GetProductForView()
                {
                    Product = _mapper.Map<ProductDto>(x)
                }).ToList().DefaultIfEmpty();

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

        public Task CreateOrEdit(CreateOrEditProductDto input)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }


    }
}
