using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Interface;
using IMSWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMSWeb.Repo
{
    public class OrderItemRepo : IOrderItem
    {
        private IMSContext _context;
        public OrderItemRepo(IMSContext context)
        {
            _context = context;

        }


        public async Task<OrderItem> CreateOrderItem(OrderItem item)
        {
            // Detach the category entity if it's being tracked
            if (_context.Entry(item).State == EntityState.Detached)
            {
                _context.Entry(item).State = EntityState.Modified; // Or EntityState.Added if it's a new entity
            }


            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }


        public async Task<List<OrderItem>> GetAllOrderItems()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemById(int id)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(X=>X.Id==id);
        }

        public async Task<bool> CreateOrderItem(OrderItemDto orderItem)
        {

            try
            {
                var orderitem = new OrderItem
                {
                    Id = orderItem.Id,
                    Price = orderItem.Price,
                    Quantity = orderItem.Quantity,
                    //  to map the 
                };

                _context.OrderItems.Add(orderitem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle exception appropriately
                return false;
            }
        }

        public async Task<bool> UpdateOrderItem(OrderItem orderItem)
        {
            
            var existingItem = await _context.OrderItems.FindAsync(orderItem.Id);
            if (existingItem == null)
            {
                // If the entity doesn't exist, return false
                return false;
            }

            // Update the properties of the existing entity with the values from the updated entity
            existingItem.Price = orderItem.Price;
    
            existingItem.Quantity = orderItem.Quantity;
            

            
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
                return false;

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            return true;
        }






    }
}
