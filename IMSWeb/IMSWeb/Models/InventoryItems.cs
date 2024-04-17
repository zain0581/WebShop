namespace IMSWeb.Models
{
    public class InventoryItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
 
        public int? IsAvailable { get; set; }
        public string? ImageUrl { get; set; }

        public int? CategoryId { get; set; }

        // Navigation properties
        public Category? Category { get; set; }

       // one invetory item can have many orderitems and supliers
        public List<OrderItem>? OrderItems { get; set; }
        public List<Supplier>? Suppliers { get; set; } // Many-to-many relationship




    }
}
