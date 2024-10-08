using StorageSystem.Application.Models.ConversionSpecs;

namespace StorageSystem.Application.Contracts.Services
{
    public interface IConversionSpecProductService
    {
        Task<Guid> CreateConversionSpecProductAsync(ConversionSpecProductCreateDto model);
        Task<Guid> UpdateConversionSpecProductAsync(ConversionSpecProductUpdateDto model);
        Task<bool> DeleteConversionSpecProductAsync(Guid id);
        Task<bool> SoftDeleteConversionSpecProductAsync(Guid id);
        Task<ConversionSpecProductForView> GetConversionSpecProductByIdAsync(Guid id);
        IEnumerable<ConversionSpecProductForView> GetAllConversionSpecProducts();
    }
}
