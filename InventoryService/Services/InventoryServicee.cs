using InventoryService.DTOs;
using InventoryService.Entities;
using InventoryService.Repositories;


namespace InventoryService.Services
{
    public class InventoryServicee : IInventoryService
    {
        private readonly IInventoryRepository _repo;
        public InventoryServicee(IInventoryRepository repo) => _repo = repo;

        public async Task<GetInventoryDto> CreateAsync(CreateInventoryDto dto)
        {
            var item = new InventoryItems
            {
                ItemName = dto.ItemName,
                Category = dto.Category,
                Quantity = dto.Quantity
            };
            await _repo.AddAsync(item);
            await _repo.SaveChangesAsync();
            return Map(item);
        }

        public async Task<GetInventoryDto?> GetByIdAsync(Guid id)
        {
            var item = await _repo.GetByIdAsync(id);
            return item is null ? null : Map(item);
        }

        public async Task<(List<GetInventoryDto>, int)> GetAllAsync(int page, int pageSize)
        {
            var (items, total) = await _repo.GetAllAsync(page, pageSize);
            return (items.Select(Map).ToList(), total);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateInventoryDto dto)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null) return false;

            item.ItemName = dto.ItemName;
            item.Category = dto.Category;
            item.Quantity = dto.Quantity;
            item.IsActive = dto.IsActive;
            item.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(item);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(Guid id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null) return false;

            item.IsActive = false;
            item.UpdatedAt = DateTime.UtcNow;
            await _repo.UpdateAsync(item);
            await _repo.SaveChangesAsync();
            return true;
        }

        private static GetInventoryDto Map(InventoryItems i) => new()
        {
            ItemId = i.ItemId,
            ItemName = i.ItemName,
            Category = i.Category,
            Quantity = i.Quantity,
            IsActive = i.IsActive,
            CreatedAt = i.CreatedAt,
            UpdatedAt = i.UpdatedAt
        };
    }
}

