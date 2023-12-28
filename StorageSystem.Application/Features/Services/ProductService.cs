using NPOI.SS.Formula.Functions;
using OneOf;
using StorageSystem.Application.Constracts.Services.Features;
using StorageSystem.Application.Contracts.DataAccess.Base;
using StorageSystem.Application.Models;
using StorageSystem.Application.Models.Bases;
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
            _unitOfWork.ProductDataAccess.Insert(product);
            return true;
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> DeleteProduct(Guid id)
        {
            _unitOfWork.ProductDataAccess.Delete(id);
            return true;
        }

        public async Task<OneOf<IEnumerable<ProductOutDto>, List<Product>, LocalizationErrorMessageOutDto, ValidationResult>> GetAllProducts(Paging filter)
        {
            List<Product> a = await _unitOfWork.ProductDataAccess.GetAllProducts1();
            return a;
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> UpdateProduct(Guid productId, Product product)
        {
            _unitOfWork.ProductDataAccess.Update(product);
            return true;
        }
    }
}
