using System.Text.Json.Serialization;

namespace IMSWeb.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        //one customer can have many orders
    
        public List<Order>? Orders { get; set; }
    }
}
