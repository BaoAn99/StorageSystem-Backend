using AutoMapper;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using OneOf;
using StorageSystem.Application.Contracts.DataAccess.Base;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models;
using StorageSystem.Application.Models.Bases;
using StorageSystem.Application.Models.Category.Outs;
using StorageSystem.Application.Models.Supplier.Ins;
using StorageSystem.Application.Models.Supplier.Outs;
using StorageSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Features.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ILogger<SupplierService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SupplierService(ILogger<SupplierService> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> CreateSupplier(CreateSupplierInsDto supplierDto)
        {
            _logger.LogInformation($"Start create supplier");
            List<Supplier> suppliers = _mapper.Map<IEnumerable<Supplier>>(supplierDto).ToList();
            try
            {
                await _unitOfWork.SupplierDataAccess.CreateSupplierRangeAsync(suppliers);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when create supplier {ex.Message}!");
                return false;
            }
            return true;
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> DeleteSupplier(Guid id)
        {
            var supplier = await _unitOfWork.SupplierDataAccess.FindSupplierById(id);
            if (supplier != null)
            {
                try
                {
                    _logger.LogInformation($"Start delete supplier");
                    _unitOfWork.SupplierDataAccess.DeleteSupplier(supplier);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error when delete supplier {ex.Message}!");
                    return false;
                }
                return true;
            }
            return new ValidationResult(
                       new List<ValidationFailure>
                       {
                            new ValidationFailure ("Not exists supplier!", "400000")
                       }
                   );
        }

        public async Task<OneOf<GetSupplierForView, LocalizationErrorMessageOutDto, ValidationResult>> FindSupplierById(Guid id)
        {
            var result = await _unitOfWork.SupplierDataAccess.FindSupplierById(id);
            if (result != null)
            {
                return _mapper.Map<GetSupplierForView>(result);
            }
            return new ValidationResult(
                       new List<ValidationFailure>
                       {
                            new ValidationFailure ("Not exists supplier!", "400000")
                       }
                   );
        }

        public async Task<OneOf<GetSupplierForView, LocalizationErrorMessageOutDto, ValidationResult>> GetAllSuppliers(FilterBase filter)
        {
            _logger.LogInformation("Start get all suppliers!");
            IEnumerable<Supplier> suppliers = await _unitOfWork.SupplierDataAccess.GetAllSuppliers(filter, true);
            GetSupplierForView data = new GetSupplierForView();
            data.Suppliers = _mapper.Map<List<SupplierList>>(suppliers);
            data.Total = _unitOfWork.SupplierDataAccess.GetTotalSuppliers(filter.Keyword);
            return data;
        }

        public async Task<OneOf<bool, LocalizationErrorMessageOutDto, ValidationResult>> UpdateSupplier(Guid supplierId, UpdateSupplierInsDto supplierDto)
        {
            Supplier supplier = await _unitOfWork.SupplierDataAccess.FindSupplierById(supplierId);
            if (supplier != null)
            {
                _logger.LogInformation($"Start update supplier");
                //supplier.Name = supplierDto.Name;

                try
                {
                    _unitOfWork.SupplierDataAccess.UpdateSupplier(supplier);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error when update supplier {ex.Message}!");
                    return false;
                }
                return true;
            }
            return new ValidationResult(
                       new List<ValidationFailure>
                       {
                            new ValidationFailure ("Not exists supplier!", "400000")
                       }
                   );
        }
    }
}
