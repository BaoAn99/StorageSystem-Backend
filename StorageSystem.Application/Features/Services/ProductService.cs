using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using OneOf;
using StorageSystem.Application.Constracts.Services.Features;
using StorageSystem.Application.Contracts.Caches;
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
        private readonly ILogger<ProductService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductCaching _productCaching;
        public ProductService(ILogger<ProductService> logger, IUnitOfWork unitOfWork, IMapper mapper, IProductCaching productCaching) 
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productCaching = productCaching;
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> CreateProduct(CreateProductInsDto productDto)
        {
            _logger.LogInformation($"Start create product");
            Product product = _mapper.Map<Product>(productDto);
            try
            {
                await _unitOfWork.ProductDataAccess.CreateProductAsync(product);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when create product {ex.Message} !");
                return false;
            }
            return true;
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> DeleteProduct(Guid id)
        {
            var product = await _unitOfWork.ProductDataAccess.FindProductById(id);
            if (product != null)
            {
                try
                {
                    _logger.LogInformation($"Start delete product");
                    _unitOfWork.ProductDataAccess.DeleteProduct(product);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error when delete product {ex.Message} !");
                    return false;
                }
                return true;
            }
            return new ValidationResult(
                       new List<ValidationFailure>
                       {
                            new ValidationFailure ("Not exists product !", "400000")
                       }
                   );
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> UpdateProduct(Guid productId, UpdateProductInsDto productDto)
        {
            Product product = await _unitOfWork.ProductDataAccess.FindProductById(productId);
            if (product != null)
            {
                _logger.LogInformation($"Start update product");
                product.Name = productDto.Name;
                product.Price = productDto.Price;
                product.Quantity = productDto.Quantity;
                product.OriginalPrice = productDto.OriginalPrice;
                product.Stock = productDto.Stock;
                product.Description = productDto.Description;
                product.CategoryId = productDto.CategoryId;
                product.ThumbnailImage = productDto.ThumbnailImage;
                product.ProductImages = productDto.ProductImages;

                try
                {
                    _unitOfWork.ProductDataAccess.UpdateProduct(product);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error when update product {ex.Message} !");
                    return false;
                }
                return true;
            }
            return new ValidationResult(
                       new List<ValidationFailure>
                       {
                            new ValidationFailure ("Not exists product !", "400000")
                       }
                   );
        }

        public async Task<OneOf<IEnumerable<GetProductForView>, LocalizationErrorMessageOutDto, ValidationResult>> GetAllProducts(Paging filter)
        {
            //await _productCaching.CachingProducts();
            //var a = _productCaching.GetCachingProducts();
            IEnumerable<Product> products = await _unitOfWork.ProductDataAccess.GetAllProducts(true);
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
