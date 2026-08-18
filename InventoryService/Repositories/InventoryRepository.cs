using InventoryService.Data;
using InventoryService.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryDbContext _context;
        public InventoryRepository(InventoryDbContext context) => _context = context;

        public async Task<InventoryItems?> GetByIdAsync(Guid id) =>
            await _context.InventoryItems.FirstOrDefaultAsync(i => i.ItemId == id && i.IsActive);

        public async Task<(List<InventoryItems>, int)> GetAllAsync(int page, int pageSize)
        {
            var query = _context.InventoryItems.Where(i => i.IsActive);
            var total = await query.CountAsync();
            var items = await query
                .OrderBy(i => i.ItemName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (items, total);
        }

        public async Task AddAsync(InventoryItems item) => await _context.InventoryItems.AddAsync(item);
        public Task UpdateAsync(InventoryItems item)
        {
            _context.InventoryItems.Update(item);
            return Task.CompletedTask;
        }

   
        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
