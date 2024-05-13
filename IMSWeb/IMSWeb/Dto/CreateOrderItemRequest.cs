using IMSWeb.Models;

namespace IMSWeb.Dto
{
    public class CreateOrderItemRequest
    {

        public int Id { get; set; }


        public int? Quantity { get; set; }
        public int? Price { get; set; }

        //navigation prperties 
        public OrderDTOO? Order { get; set; }
        public InventoryItemDTOO? InventoryItem { get; set; }
    }
}
