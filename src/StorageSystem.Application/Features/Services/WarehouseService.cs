using StorageSystem.Application.Contracts.Repositories;
using StorageSystem.Application.Contracts.Repositories.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Warehouses;
using StorageSystem.Domain.Commons.Interfaces;
using StorageSystem.Domain.Entities.Warehouses;

namespace StorageSystem.Application.Features.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository<Warehouse, Guid> _warehouseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityManager<Warehouse> _entityManager;

        public WarehouseService(IWarehouseRepository<Warehouse, Guid> warehouseRepository, IEntityManager<Warehouse> entityManager, IUnitOfWork unitOfWork)
        {
            _warehouseRepository = warehouseRepository;
            _entityManager = entityManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateWarehouseAsync(WarehouseCreateDto model)
        {
            try
            {
                var warehouse = new Warehouse()
                {
                    Name = model.Name,
                    Address = model.Address,
                    ContactName = model.ContactName,
                    Phone = model.Phone,
                    Description = model.Description,
                };
                _entityManager.SetCreating(warehouse);
                await _warehouseRepository.CreateAsync(warehouse);

                await _unitOfWork.CommitAsync();
                return warehouse.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteWarehouseAsync(Guid id)
        {
            try
            {
                var warehouse = await _warehouseRepository.GetByIdAsync(id);
                if (warehouse != null)
                {
                    await _warehouseRepository.DeleteAsync(warehouse);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public IEnumerable<WarehouseForView> GetAllWarehouses() => throw new NotImplementedException();

        public Task<WarehouseForView> GetWarehouseByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateWarehouseAsync(WarehouseUpdateDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteWarehouseAsync(Guid id)
        {
            try
            {
                var warehouse = await _warehouseRepository.GetByIdAsync(id);
                if (warehouse != null)
                {
                    warehouse.IsDeleted = true;
                    warehouse.IsPublished = false;
                    await _warehouseRepository.UpdateAsync(warehouse);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
