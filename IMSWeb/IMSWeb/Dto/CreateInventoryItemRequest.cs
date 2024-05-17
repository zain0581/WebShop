using IMSWeb.Models;

namespace IMSWeb.Dto
{
    public class CreateInventoryItemRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Qty { get; set; }
        public string? ImageUrl { get; set; }
        public int? UnitPrice { get; set; }
        public CategoryDto? Category { get; set; }
        public SupplierDTO? Suppliers { get; set; }

    }
}
