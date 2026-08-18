using InventoryService.DTOs;

namespace InventoryService.Services
{
    public interface IInventoryService
    {
        Task<GetInventoryDto> CreateAsync(CreateInventoryDto dto);
        Task<GetInventoryDto?> GetByIdAsync(Guid id);
        Task<(List<GetInventoryDto> Items, int TotalCount)> GetAllAsync(int page, int pageSize);
        Task<bool> UpdateAsync(Guid id, UpdateInventoryDto dto);
        Task<bool> SoftDeleteAsync(Guid id);
    }
}
