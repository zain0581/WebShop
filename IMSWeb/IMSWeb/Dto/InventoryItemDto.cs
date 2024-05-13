namespace IMSWeb.Dto
{
    public class InventoryItemDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? IsAvailable { get; set; }
        public string? ImageUrl { get; set; }
        //public int SupplierId { get; set; }

        //public int? CategoryId { get; set; }


        public CategoryDto? category { get; set; }
        //public CreateSupplierDto? supplier { get; set; }
    }
}
