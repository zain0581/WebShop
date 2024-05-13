using System.Text.Json.Serialization;

namespace IMSWeb.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }

        public float? GrossPrice { get; set; }

        public float? Tax { get; set; }
        public float? TotalPrice { get; set; }

        public string? Description { get; set; }

        // one order can have many orderitems
        [JsonIgnore]
        public List<OrderItem>? OrderItems { get; set; }

        // Navigation property for Customer
        public Customer? Customer { get; set; }








    }
}
