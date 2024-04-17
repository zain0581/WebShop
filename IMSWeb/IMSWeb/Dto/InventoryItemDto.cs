namespace IMSWeb.Dto
{
    public class InventoryItemDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? IsAvailable { get; set; }
        public string? ImageUrl { get; set; }

        public CategoryDto? Category { get; set; }
        public List<SupplierDTO>? Suppliers { get; set; }
    }
}
