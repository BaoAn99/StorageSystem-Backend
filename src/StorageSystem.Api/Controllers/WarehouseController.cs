using Microsoft.AspNetCore.Mvc;
using StorageSystem.Application.Contracts.Services;
using StorageSystem.Application.Models.Warehouses;

namespace StorageSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] WarehouseCreateDto model)
        {
            var warehouseId = await _warehouseService.CreateWarehouseAsync(model);
            return Ok(warehouseId);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] WarehouseUpdateDto model)
        {
            var warehouseId = await _warehouseService.UpdateWarehouseAsync(model);
            return Ok(warehouseId);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _warehouseService.DeleteWarehouseAsync(id);
            return Ok(isDeleted);
        }

        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var isDeleted = await _warehouseService.SoftDeleteWarehouseAsync(id);
            return Ok(isDeleted);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            return Ok(warehouse);
        }

        [HttpPost("GetAll")]
        public ActionResult GetAll()
        {
            var warehouses = _warehouseService.GetAllWarehouses();
            return Ok(warehouses);
        }
    }
}
