using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using OneOf;
using StorageSystem.Application.Constracts.Services.Features;
using StorageSystem.Application.Contracts.DataAccess.Base;
using StorageSystem.Application.Models;
using StorageSystem.Application.Models.Bases;
using StorageSystem.Application.Models.Product.Ins;
using StorageSystem.Application.Models.Product.Outs;
using StorageSystem.Domain.Entities;

namespace StorageSystem.Application.Features.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> CreateProduct(CreateProductInsDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            var result = await _unitOfWork.ProductDataAccess.CreateProductAsync(product);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return new ValidationResult (
                       new List<ValidationFailure>
                       {
                            new ValidationFailure ("You have wrong at server !", "5000000")
                       }
                   );
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> DeleteProduct(Guid id)
        {
            var result = await FindProductById(id);
            if(result.IsT0)
            {
                await _unitOfWork.ProductDataAccess.DeleteProduct(id);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return result.AsT2;
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> UpdateProduct(Guid productId, UpdateProductInsDto productDto)
        {
            Product product = new Product();
            //_unitOfWork.ProductDataAccess.Update(product);
            return true;
        }

        public async Task<OneOf<IEnumerable<GetProductForView>, LocalizationErrorMessageOutDto, ValidationResult>> GetAllProducts(Paging filter)
        {
            IEnumerable<Product> products = await _unitOfWork.ProductDataAccess.GetAllProducts();
            IEnumerable<GetProductForView> data = _mapper.Map<IEnumerable<GetProductForView>>(products);
            return data.ToList();
        }

        public async Task<OneOf<GetProductForView, LocalizationErrorMessageOutDto, ValidationResult>> FindProductById(Guid id)
        {
            var result = await _unitOfWork.ProductDataAccess.FindProductById(id);
            if(result != null)
            {
                return _mapper.Map<GetProductForView>(result);
            }
            return new ValidationResult(
                       new List<ValidationFailure>
                       {
                            new ValidationFailure ("Not exists product !", "400000")
                       }
                   );
        }
    }
}
