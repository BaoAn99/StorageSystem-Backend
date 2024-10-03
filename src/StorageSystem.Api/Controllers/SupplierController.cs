using Microsoft.AspNetCore.Mvc;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Supplier;

namespace StorageSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierCreateDto model)
        {
            var supplierId = await _supplierService.CreateSupplierAsync(model);
            return Ok(supplierId);
        }
    }
}
