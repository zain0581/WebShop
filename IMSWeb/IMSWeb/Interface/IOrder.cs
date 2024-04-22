using IMSWeb.Dto;
using IMSWeb.Models;

namespace IMSWeb.Interface
{
    public interface IOrder
    {
       public Task<List<Order>> GetAllOrders();
       public Task<Order> GetOrderById(int id);
        public Task<bool> CreateOrder(OrderDto orderDto);
       public Task<bool> UpdateOrder(Order order);
       public Task<bool> DeleteOrder(int id);
    }
}
