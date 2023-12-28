using OneOf;
using OneOf.Types;
using StorageSystem.Application.Models;
using StorageSystem.Application.Models.Bases;
using StorageSystem.Application.Models.Products.Out;
using StorageSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Constracts.Services.Features
{
    public interface IProduct
    {
        Task<OneOf<IEnumerable<ProductOutDto>, List<Product>, LocalizationErrorMessageOutDto, ValidationResult>> GetAllProducts(Paging filter);

        Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> CreateProduct(Product product);
        
        Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> UpdateProduct(Guid productId, Product product);
        
        Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> DeleteProduct(Guid id);
    }
}
