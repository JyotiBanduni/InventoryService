using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    using InventoryService.DTOs;
    using InventoryService.Services;  
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class InventoryItemsController : ControllerBase
    {
        private readonly IInventoryService _service;
        public InventoryItemsController(IInventoryService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create(CreateInventoryDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.ItemId }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var (items, total) = await _service.GetAllAsync(page, pageSize);
            return Ok(new { Total = total, Page = page, PageSize = pageSize, Items = items });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateInventoryDto dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var ok = await _service.SoftDeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
