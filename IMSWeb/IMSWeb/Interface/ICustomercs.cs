using IMSWeb.Dto;
using IMSWeb.Models;

namespace IMSWeb.Interface
{
    public interface ICustomercs
    {
       public Task<List<Customer>> GetAllCustomers();
       public Task<Customer> GetCustomerById(int id);
   
        public Task<bool> CreateCustomer(CustomerDto customerDto);
       public Task<bool> UpdateCustomer(Customer customer);
       public Task<bool> DeleteCustomer(int id);
    }
}
