using InventoryService.Entities;

namespace InventoryService.Repositories
{
    public interface IInventoryRepository
    {
        Task<InventoryItems?> GetByIdAsync(Guid id);
        Task<(List<InventoryItems> Items, int TotalCount)> GetAllAsync(int page, int pageSize);
        Task AddAsync(InventoryItems item);
        Task UpdateAsync(InventoryItems item);
        Task SaveChangesAsync();
    }
}
