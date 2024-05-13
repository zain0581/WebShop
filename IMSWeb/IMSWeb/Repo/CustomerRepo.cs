using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Interface;
using IMSWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace IMSWeb.Repo
{
    public class CustomerRepo : ICustomercs
    {

        private IMSContext _context;

        public CustomerRepo(IMSContext context)
        {
            _context = context;

            //var datalist = from p in _context.InventoryItems
            //               on 
        }
        public async Task<bool> CreateCustomer(CustomerDto customerDto)
        {
            var customer = new Customer
            {
                Id = customerDto.Id,
                Name = customerDto.Name,
                Email = customerDto.Email,  
                Phone = customerDto.Phone,
             
                // Map properties accordingly
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
