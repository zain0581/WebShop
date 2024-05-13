using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Interface;
using IMSWeb.Models;
using IMSWeb.Repo;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace unittest.RepoTest
{
    public class CustomerTest
    {
        DbContextOptions<IMSContext> options;
        IMSContext _dbContext;

        public CustomerTest()
        {
            options = new DbContextOptionsBuilder<IMSContext>()
                .UseInMemoryDatabase(databaseName: "dummydb")
                .Options;

            _dbContext = new IMSContext(options);

            _dbContext.Customers.AddRange(
                new Customer { Id = 1, Name = "Customer 1", Email = "customer1@example.com", Phone = "123456789" },
                new Customer { Id = 2, Name = "Customer 2", Email = "customer2@example.com", Phone = "987654321" },
                new Customer { Id = 3, Name = "Customer 3", Email = "customer3@example.com", Phone = "898989" }
            );

            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetAllCustomers_ReturnsAllCustomers()
        {
            // Arrange
            CustomerRepo repo = new CustomerRepo(_dbContext);

            // Act
            var res = await repo.GetAllCustomers();
            var actual = 3;

            // Assert
            Assert.Equal(actual, res.Count);
        }

        [Fact]
        public async Task GetCustomerById_Exists_ReturnsCorrectCustomer()
        {
            // Arrange
            CustomerRepo repo = new CustomerRepo(_dbContext);

            // Act
            var res = await repo.GetCustomerById(2);

            // Assert
            Assert.Equal(2, res.Id);
        }

        [Fact]
        public async Task DeleteCustomer_Exists_ReturnsTrue()
        {
            // Arrange
            ICustomercs repo = new CustomerRepo(_dbContext);

            // Act
            var res = await repo.DeleteCustomer(2);

            // Assert
            Assert.True(res);
        }

        [Fact]
        public async Task UpdateCustomer_Exists_ReturnsTrue()
        {
            // Arrange
            CustomerRepo repo = new CustomerRepo(_dbContext);
            var customer = new Customer
            {
              Id = 2,
                Name = "Updated Customer",
                Email = "updated@example.com",
                Phone = "999999999"
            };

            // Act
            var result = await repo.UpdateCustomer(customer);

            // Assert
            Assert.True(result);
            //var updatedCustomer = await _dbContext.Customers.FindAsync(customer.Id);
            //Assert.NotNull(updatedCustomer);
        }

        [Fact]
        public async Task CreateCustomer_Exists_ReturnsNotNull()
        {
            // Arrange
            CustomerRepo repo = new CustomerRepo(_dbContext);
            var customerDto = new CustomerDto
            {
                Id= 7,
                Name = "New Customer",
                Email = "newcustomer@example.com",
                Phone = "123456789"
            };

            // Act
            var result = await repo.CreateCustomer(customerDto);

            // Assert
            Assert.NotNull(result);
        }
    }
}
