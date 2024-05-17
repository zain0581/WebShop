using System.Text.Json.Serialization;

namespace IMSWeb.Models
{
    public class InventoryItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Qty { get; set; }
        public string? ImageUrl { get; set; }
        public int? UnitPrice { get; set; }

        public Category? Category { get; set; }
        public Supplier? Suppliers { get; set; }

       
        public List<OrderItem>? OrderItems { get; set; }




    }
}
