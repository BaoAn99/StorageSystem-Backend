using OneOf;
using StorageSystem.Application.Models;
using StorageSystem.Application.Models.Product.Outs;
using StorageSystem.Application.Models.Product.Ins;
using StorageSystem.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using StorageSystem.Application.Models.Bases;

namespace StorageSystem.Application.Constracts.Services.Features
{
    public interface IProductService
    {
        Task<OneOf<IEnumerable<GetProductForView>, LocalizationErrorMessageOutDto, ValidationResult>> GetAllProducts(Paging filter);

        Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> CreateProduct(CreateProductInsDto productDto);
        
        Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> UpdateProduct(Guid productId, UpdateProductInsDto productDto);
        
        Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> DeleteProduct(Guid id);
    }
}
