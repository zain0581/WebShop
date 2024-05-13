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


        //public async Task<bool> CreateOrder(Order order)
        //{
        //    _context.Orders.Add(order);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> CreateOrder(OrderDto orderDto)
        {
            try
            {
                Customer customer;

                // Check if the customer already exists
                if (orderDto.Customer.Id != 0)
                {
                    // Retrieve the existing customer from the database
                    customer = await _context.Customers.FindAsync(orderDto.Customer.Id);
                    if (customer == null)
                    {
                        // Customer not found, handle the error or throw an exception
                        throw new Exception("Customer not found.");
                    }
                }
                else
                {
                    // Customer does not exist, create a new customer
                    customer = new Customer
                    {
                        Name = orderDto.Customer.Name,
                        Email = orderDto.Customer.Email,
                        Phone = orderDto.Customer.Phone,
                        // Set other properties accordingly
                    };
                    _context.Customers.Add(customer);
                }

                // Map OrderDto to Order entity
                var order = new Order
                {
                    OrderNo = orderDto.OrderNo,
                    OrderDate = orderDto.OrderDate,
                    GrossPrice = orderDto.GrossPrice,
                    Tax = orderDto.Tax,
                    TotalPrice = orderDto.TotalPrice,
                    Description = orderDto.Description,
                    Customer = customer,
                    
                  
                    // Set other properties accordingly
                };
                _context.Orders.Add(order);

                // Create order items
                //foreach (var itemDto in orderDto.OrderItems)
                //{
                //    var orderItem = new OrderItem
                //    {
                //        Quantity = itemDto.Quantity,
                //        Order = order
                //        // Map other properties accordingly
                //    };
                //    _context.OrderItems.Add(orderItem);
                //}

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception (log, report, etc.)
                return false;
            }
        }





    public async Task<bool> DeleteOrder(int id)
        {
            var deletingorder = await _context.Orders. Include(c=>c.Customer).FirstOrDefaultAsync(o => o.Id == id);
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
           return await _context.Orders.Include(x=>x.Customer).ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
           return await _context.Orders. Include(m=>m.Customer).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            try
            {
                // Retrieve the existing order from the database
                var existingOrder = await _context.Orders.FindAsync(order.Id);
                if (existingOrder == null)
                {
                    // If the order doesn't exist, return false
                    return false;
                }

                // Update the properties of the existing order with the values from the updated order
                existingOrder.OrderNo = order.OrderNo;
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.GrossPrice = order.GrossPrice;
                existingOrder.Tax = order.Tax;
                existingOrder.TotalPrice = order.TotalPrice;
                existingOrder.Description = order.Description;
                // Update other properties as needed

                // Save the changes
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle exceptions appropriately
                return false;
            }
        }

    }
}
