using IMSWeb.Dal;
using IMSWeb.Dto;
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

        public async Task<bool> CreateOrder(OrderDto orderDto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Map CustomerDto to Customer entity
                    var customer = new Customer
                    {
                        Name = orderDto.Customer.Name,
                        Email = orderDto.Customer.Email
                        // Set other properties accordingly
                    };
                    _context.Customers.Add(customer);

                    // Map OrderDto to Order entity
                    var order = new Order
                    {
                        OrderNo = orderDto.OrderNo,
                        OrderDate = orderDto.OrderDate,
                        GrossPrice = orderDto.GrossPrice,
                        Tax = orderDto.Tax,
                        TotalPrice = orderDto.TotalPrice,
                        Description = orderDto.Description,
                        Customer = customer
                        // Set other properties accordingly
                    };
                    _context.Orders.Add(order);

                    // Create order items
                    foreach (var itemDto in orderDto.OrderItems)
                    {
                        var orderItem = new OrderItem
                        {
                            Quantity = itemDto.Quantity,
                          
                            Order = order
                            // Map other properties accordingly
                        };
                        _context.OrderItems.Add(orderItem);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
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
