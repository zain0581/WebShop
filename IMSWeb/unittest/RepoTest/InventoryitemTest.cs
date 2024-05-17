using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Models;
using IMSWeb.Repo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace unittest.RepoTest
{
    public class InventoryitemTest
    {
        DbContextOptions<IMSContext> options;
        IMSContext _dbContext;

        public InventoryitemTest()
        {
            options = new DbContextOptionsBuilder<IMSContext>()
                .UseInMemoryDatabase(databaseName: "dummydb")
                .Options;

            _dbContext = new IMSContext(options);

            _dbContext.InventoryItems.AddRange(
                new InventoryItems { Id = 1, Name = "Item 1", Description = "Description 1", Qty = 1, ImageUrl = "image1.jpg" },
                new InventoryItems { Id = 2, Name = "Item 2", Description = "Description 2", Qty = 1, ImageUrl = "image2.jpg" },
                new InventoryItems { Id = 3, Name = "Item 3", Description = "Description 3", Qty = 1, ImageUrl = "image3.jpg" }
            );

            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task CreateInventoryItem_Exists_ReturnsNotNull()
        {
            // Arrange
            InventoryItemsRepo repo = new InventoryItemsRepo(_dbContext);

            
            int categoryId = 1; // Example category ID
            int supplierId = 1; // Example supplier ID

            var itemDto = new InventoryItems
            {
                Id = 8,
                Name = "New Item",
                Description = "New Description",
                Qty = 2,
                ImageUrl = "newimage.jpg",
                Category = new Category { Id = categoryId }, // Set category using navigation property
                Suppliers = new Supplier { Id = supplierId } // Set supplier using navigation property
            };

            // Act
            var result = await repo.CreateInventoryItem(itemDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(itemDto.Name, result.Name);
    
        }


        [Fact]
        public async Task DeleteInventoryItem_Exists_ReturnsTrue()
        {
            // Arrange
            InventoryItemsRepo repo = new InventoryItemsRepo(_dbContext);

            // Act
            var result = await repo.DeleteInventoryItem(2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllInventoryItems_ReturnsAllItems()
        {
            // Arrange
            InventoryItemsRepo repo = new InventoryItemsRepo(_dbContext);
            var expectedCount = 3;

            // Act
            var result = await repo.GetAllInventoryItems();

            // Assert
            Assert.Equal(expectedCount, result.Count);
        }

        [Fact]
        public async Task GetInventoryItemById_Exists_ReturnsCorrectItem()
        {
            // Arrange
            InventoryItemsRepo repo = new InventoryItemsRepo(_dbContext);
            var expectedId = 2;

            // Act
            var result = await repo.GetInventoryItemById(expectedId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedId, result.Id);
        }

        [Fact]
        public async Task GetInventoryItemByName_Exists_ReturnsCorrectItem()
        {
            // Arrange
            InventoryItemsRepo repo = new InventoryItemsRepo(_dbContext);
            var expectedName = "Item 1";

            // Act
            var result = await repo.GetInventoryItemByName(expectedName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedName, result.Name);
        }

        [Fact]
        public async Task UpdateInventoryItemWithRelations_Exists_ReturnsTrue()
        {
            // Arrange
            InventoryItemsRepo repo = new InventoryItemsRepo(_dbContext);
            var itemId = 1;
            var itemDto = new CreateInventoryItemRequest
            {
                Id=2,
                Name = "Updated Item",
                Description = "Updated Description",
                Qty = 1,
                ImageUrl = "updatedimage.jpg",
                //Category = new CategoryDto { Id = 1, Name = "Updated Category", Description = "Updated Description", IsActive = 1, ImageUrl = "updatedcategory.jpg" },
                //Suppliers = new SupplierDTO { Id = 1, Name = "Updated Supplier", Email = "updatedsupplier@example.com", Phone = "9876543210" }
            };

            // Act
            var result = await repo.UpdateInventoryItemWithRelations(itemId, itemDto);

            // Assert
            Assert.True(result);
        }
    }
}
