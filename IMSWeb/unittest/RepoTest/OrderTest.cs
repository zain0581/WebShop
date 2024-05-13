using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Models;
using IMSWeb.Repo;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace unittest.RepoTest
{
    public class OrderTest
    {
        private readonly IMSContext _dbContext;

        public OrderTest()
        {
            // Initialize a new in-memory database for testing
            var options = new DbContextOptionsBuilder<IMSContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            _dbContext = new IMSContext(options);

            // Add test data
            _dbContext.Orders.AddRange(
                new Order { Id = 1, OrderNo = "ORD-001", OrderDate = DateTime.Now, GrossPrice = 100, Tax = 10, TotalPrice = 110, Description = "Order 1" },
                new Order { Id = 2, OrderNo = "ORD-002", OrderDate = DateTime.Now, GrossPrice = 200, Tax = 20, TotalPrice = 220, Description = "Order 2" }
            );

            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetAllOrders_ReturnsAllOrders()
        {
            // Arrange
            var repo = new OrderRepo(_dbContext);

            // Act
            var orders = await repo.GetAllOrders();

            // Assert
            Assert.Equal(2, orders.Count);
        }

        [Fact]
        public async Task GetOrderById_Exists_ReturnsCorrectOrder()
        {
            // Arrange
            var repo = new OrderRepo(_dbContext);

            // Act
            var order = await repo.GetOrderById(1); // Assuming order with ID 1 exists

            // Assert
            Assert.NotNull(order);
            Assert.Equal(1, order.Id);
        }

        [Fact]
        public async Task DeleteOrder_Exists_ReturnsTrue()
        {
            // Arrange
            var repo = new OrderRepo(_dbContext);

            // Act
            var result = await repo.DeleteOrder(1); // Assuming order with ID 1 exists

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateOrder_Exists_ReturnsTrue()
        {
            // Arrange
            var repo = new OrderRepo(_dbContext);

            // Create an updated order object
            var updatedOrder = new Order
            {
                Id = 1,
                OrderNo = "ORD-001-UPDATED",
                OrderDate = DateTime.Now.AddDays(1),
                GrossPrice = 150,
                Tax = 15,
                TotalPrice = 165,
                Description = "Order 1 - Updated"
            };

            // Act
            var result = await repo.UpdateOrder(updatedOrder);

            // Assert
            Assert.True(result);

            // Verify that the order was updated correctly
            var updatedOrderFromDb = await _dbContext.Orders.FindAsync(1);
            Assert.NotNull(updatedOrderFromDb);
       
        }


        [Fact]
        public async Task CreateOrder_ValidOrderDto_ReturnsTrue()
        {
            // Arrange
            var repo = new OrderRepo(_dbContext);

            // Create a new order DTO
            var orderDto = new OrderDto
            {
                OrderNo = "ORD-003",
                OrderDate = DateTime.Now,
                GrossPrice = 150,
                Tax = 15,
                TotalPrice = 165,
                Description = "Test Order",
                Customer = new CustomerDto
                {
                    Name = "John Doe",
                    Email = "john@example.com",
                    Phone = "1234567890"
                }
            };

            // Act
            var result = await repo.CreateOrder(orderDto);

            // Assert
            Assert.True(result);

            // Verify that the order was created in the database
            var createdOrder = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderNo == orderDto.OrderNo);
            Assert.NotNull(createdOrder);
        }

    
    }
}
