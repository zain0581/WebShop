using IMSWeb.Dto;
using IMSWeb.Models;

namespace IMSWeb.Interface
{
    public interface IOrderItem

    {
        public Task<OrderItem> CreateOrderItem(OrderItem item);
        public Task<List<OrderItem>> GetAllOrderItems();
        public Task<OrderItem> GetOrderItemById(int id);
        public Task<bool> CreateOrderItem(OrderItemDto orderItem);
        public Task<bool> UpdateOrderItem(OrderItem orderItem);
        public Task<bool> DeleteOrderItem(int id);
    }
}
