using IMSWeb.Models;

namespace IMSWeb.Interface
{
    public interface IOrder
    {
       public Task<List<Order>> GetAllOrders();
       public Task<Order> GetOrderById(int id);
       public Task<bool> CreateOrder(Order order);
       public Task<bool> UpdateOrder(Order order);
       public Task<bool> DeleteOrder(int id);
    }
}
