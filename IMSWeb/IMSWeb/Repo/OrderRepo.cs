using IMSWeb.Dal;
using IMSWeb.Interface;
using IMSWeb.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace IMSWeb.Repo
{
    public class OrderRepo : IOrder
    {
        private IMSContext _context;
        public OrderRepo(IMSContext context) 
        {
            _context = context;
        }

        public async Task<bool> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var deletingorder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (deletingorder != null)
            {
                 _context.Remove(deletingorder);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Order>> GetAllOrders()
        {
           return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
           return await _context.Orders.FindAsync(id);
           
           
        }

        public Task<bool> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
