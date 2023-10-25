using OneOf;
using StorageSystem.Application.Constracts.Services.Features;
using StorageSystem.Application.Contracts.DataAccess.Base;
using StorageSystem.Application.Models;
using StorageSystem.Application.Models.Products.Ins;
using StorageSystem.Application.Models.Products.Out;
using StorageSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Features.Services
{
    public class ProductService : IProduct
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> CreateProduct(Product product)
        {
            await _unitOfWork.ProductDataAccess.CreateProduct(product);
            return true;
        }

        public Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<IEnumerable<ProductOutDto>, LocalizationErrorMessageOutDto, ValidationResult>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
