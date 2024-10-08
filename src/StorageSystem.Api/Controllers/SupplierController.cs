using Microsoft.AspNetCore.Mvc;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Suppliers;

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

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] SupplierCreateDto model)
        {
            var supplierId = await _supplierService.CreateSupplierAsync(model);
            return Ok(supplierId);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] SupplierUpdateDto model)
        {
            var supplierId = await _supplierService.UpdateSupplierAsync(model);
            return Ok(supplierId);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _supplierService.DeleteSupplierAsync(id);
            return Ok(isDeleted);
        }

        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var isDeleted = await _supplierService.SoftDeleteSupplierAsync(id);
            return Ok(isDeleted);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            return Ok(supplier);
        }

        [HttpPost("GetAll")]
        public ActionResult GetAll()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            return Ok(suppliers);
        }
    }
}
