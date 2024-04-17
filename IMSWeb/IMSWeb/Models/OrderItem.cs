namespace IMSWeb.Models
{
    public class OrderItem

    {
        public int Id { get; set; }
        public int? OrderId { get; set; }

        public int? InventoryId { get; set; }

        public int? Quantity { get; set; }
        public int? Price { get; set; }

        //navigation prperties 
        public Order? Order { get; set; }
        public InventoryItems? InventoryItem { get; set; }
    }
}
