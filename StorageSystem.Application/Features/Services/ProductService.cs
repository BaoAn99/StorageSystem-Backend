using OneOf;
using StorageSystem.Application.Constracts.Services.Features;
using StorageSystem.Application.Contracts.DataAccess.Base;
using StorageSystem.Application.Models;
using StorageSystem.Application.Models.Bases;
using StorageSystem.Application.Models.Product.Ins;
using StorageSystem.Application.Models.Product.Outs;
using StorageSystem.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace StorageSystem.Application.Features.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> CreateProduct(CreateProductInsDto productDto)
        {
            Product product = new Product();
            _unitOfWork.ProductDataAccess.Insert(product);
            return true;
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> DeleteProduct(Guid id)
        {
            _unitOfWork.ProductDataAccess.Delete(id);
            return true;
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> UpdateProduct(Guid productId, UpdateProductInsDto productDto)
        {
            Product product = new Product();
            _unitOfWork.ProductDataAccess.Update(product);
            return true;
        }

        public async Task<OneOf<IEnumerable<GetProductForView>, LocalizationErrorMessageOutDto, ValidationResult>> GetAllProducts(Paging filter)
        {
            //IEnumerable<GetProductForView> a = await _unitOfWork.ProductDataAccess.GetAllProducts1();
            IEnumerable<GetProductForView> a = new List<GetProductForView>();
            return a.ToList();
        }
    }
}
