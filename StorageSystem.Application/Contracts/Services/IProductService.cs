using OneOf;
using StorageSystem.Application.Models;
using StorageSystem.Application.Models.Product.Outs;
using StorageSystem.Application.Models.Product.Ins;
using StorageSystem.Domain.Entities;
using StorageSystem.Application.Models.Bases;
using FluentValidation.Results;

namespace StorageSystem.Application.Constracts.Services.Features
{
    public interface IProductService
    {
        Task<OneOf<GetProductForView, LocalizationErrorMessageOutDto, ValidationResult>> GetAllProducts(Paging filter);

        Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> CreateProduct(CreateProductInsDto productDto);
        
        Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> UpdateProduct(Guid productId, UpdateProductInsDto productDto);
        
        Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> DeleteProduct(Guid id);

        Task<OneOf<GetProductForView, LocalizationErrorMessageOutDto, ValidationResult>> FindProductById(Guid id);
    }
}
