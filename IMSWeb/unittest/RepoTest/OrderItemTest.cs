using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Models;
using IMSWeb.Repo;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace unittest.RepoTest
{
    public class OrderItemTest
    {
        private readonly IMSContext _dbContext;

        public OrderItemTest()
        {
            // Initialize a new in-memory database for testing
            var options = new DbContextOptionsBuilder<IMSContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            _dbContext = new IMSContext(options);

            // Add test data
            _dbContext.OrderItems.AddRange(
                new OrderItem { Id = 1, Price = 10, Quantity = 2 },
                new OrderItem { Id = 2, Price = 20, Quantity = 3 }
            );

            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetAllOrderItems_ReturnsAllOrderItems()
        {
            // Arrange
            var repo = new OrderItemRepo(_dbContext);

            // Act
            var orderItems = await repo.GetAllOrderItems();

            // Assert
            Assert.Equal(2, orderItems.Count);
        }

        [Fact]
        public async Task GetOrderItemById_Exists_ReturnsCorrectOrderItem()
        {
            // Arrange
            var repo = new OrderItemRepo(_dbContext);

            // Act
            var orderItem = await repo.GetOrderItemById(1); // Assuming order item with ID 1 exists

            // Assert
            Assert.NotNull(orderItem);
            Assert.Equal(1, orderItem.Id);
        }

        [Fact]
        public async Task CreateOrderItem_Exists_ReturnsNotNull()
        {
            // Arrange
            var repo = new OrderItemRepo(_dbContext);
            var orderItemDto = new OrderItemDto
            {
                Id = 3,
                Price = 30,
                Quantity = 4
               
            };

            // Act
            var result = await repo.CreateOrderItem(orderItemDto);

            // Assert
            Assert.NotNull(result);
       
        }

        [Fact]
        public async Task DeleteOrderItem_Exists_ReturnsTrue()
        {
            // Arrange
            var repo = new OrderItemRepo(_dbContext);

            // Act
            var result = await repo.DeleteOrderItem(1); // Assum

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateOrderItem_Exists_ReturnsTrue()
        {
            // Arrange
            var repo = new OrderItemRepo(_dbContext);
            var updatedOrderItem = new OrderItem { Id = 1, Price = 15, Quantity = 3 };

            // Act
            var result = await repo.UpdateOrderItem(updatedOrderItem);

            // Assert
            Assert.True(result);

            // Check if the order item was updated correctly
            var retrievedOrderItem = await _dbContext.OrderItems.FindAsync(updatedOrderItem.Id);
            Assert.NotNull(retrievedOrderItem);
            Assert.Equal(updatedOrderItem.Price, retrievedOrderItem.Price);
            Assert.Equal(updatedOrderItem.Quantity, retrievedOrderItem.Quantity);
        }

    }
}
