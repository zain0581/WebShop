using IMSWeb.Models;

namespace IMSWeb.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }

        public float? GrossPrice { get; set; }

        public float? Tax { get; set; }
        public float? TotalPrice { get; set; }

        public string? Description { get; set; }
        public CustomerDto Customer { get; set; }
        //public List<OrderItemDto> OrderItems { get; set; }

        //public CustomerDto Customer { get; set; }
        //public List<OrderItemDto> OrderItems { get; set; }
    }
}
