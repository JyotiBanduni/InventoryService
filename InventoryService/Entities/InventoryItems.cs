namespace InventoryService.Entities
{
    public class InventoryItems
    {
        public Guid ItemId { get; set; } = Guid.NewGuid();
        public string ItemName { get; set; } = string.Empty;
        public string? Category { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
