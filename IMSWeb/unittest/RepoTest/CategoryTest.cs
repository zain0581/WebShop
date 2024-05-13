using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Models;
using IMSWeb.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unittest.RepoTest
{
    public class CategoryTest
    {
        DbContextOptions<DbContext> options;
        IMSContext _dbContext;

        #region
        // Arrange - varable creation 
        // Act - call method
        //Assert - verify i get the right result
        #endregion
        public CategoryTest()
        {
            options = new DbContextOptionsBuilder<DbContext>()
                .UseInMemoryDatabase(databaseName: "dummydb")
                .Options;
       
            _dbContext= new IMSContext(options);
            //_dbContext.Database.EnsureDeleted();

            Category category1 = new Category()
            {
                Id = 1,
                Description = "Test",
                ImageUrl = "test",
                IsActive = 1,
                Name = "Test",

            };
            Category category2 = new Category()
            {
                Id = 2,
                Description = "Test2",
                ImageUrl = "test2",
                IsActive = 0,
                Name = "Test2",

            };

            Category category3 = new Category()
            {
                Id = 13,
                Description = "Test3",
                ImageUrl = "test3",
                IsActive = 1,
                Name = "Test3",

            };

            _dbContext.Categories.Add(category1);
            _dbContext.Categories.Add(category2);
            _dbContext.Categories.Add(category3);
            _dbContext.SaveChanges();

   






        }

        [Fact]
        public async Task getallcategory()
        {


            CategoryRepo repo = new CategoryRepo(_dbContext);



            var res =   await repo.GetAllCategories();
            var actual = 3;

            Assert.Equal(actual, res.Count);

    
        }

        [Fact]
        public async Task getallcategorybyid_exsist()
        {


            CategoryRepo repo = new CategoryRepo(_dbContext);



            var res = await repo.GetCategoryByIdWithItem(13);
            

            Assert.Equal(13, res.Id);

       
        }



        [Fact]
        public async Task getallcategorybyname_exsist()
        {


            CategoryRepo repo = new CategoryRepo(_dbContext);



            var res = await repo.GetCategoryByName("Test2");
            var act = "Test2";

            Assert.Equal(act, res.Name);

            #region
            // Arrange - varable creation 
            // Act - call method
            //Assert - verify i get the right result
            #endregion
        }

        [Fact]
        public async Task deletecategorybyname_exsist()
        {


            CategoryRepo repo = new CategoryRepo(_dbContext);

            var res = await repo.DeleteCategory(2);
            //var res1 = await repo.DeleteCategory(4);


            Assert.True(res);
            //Assert.True(!res1);

          
        }

        [Fact]
        public async Task createcategory_exsist()
        {


            CategoryRepo repo = new CategoryRepo(_dbContext);

            var res = new CategoryDto()
            {
                Name = "Test",
                Id = 6,
                ImageUrl = "de",
                Description = "Test",
                IsActive = 1,
            };
            var resf = await repo.CreateCategory(res);




            Assert.NotNull(resf);
          


            #region
            // Arrange - varable creation 
            // Act - call method
            //Assert - verify i get the right result
            #endregion
        }

        [Fact]
        public async Task UpdateCategory_Exists()
        {
            // Arrange
            CategoryRepo repo = new CategoryRepo(_dbContext);
            var categoryDto = new CategoryDto
            {
                Id = 1,
                Name = "Updated Category",
                Description = "Updated Description",
                IsActive = 1,
                ImageUrl = "updated.jpg",
        
            };

            // Act
            var result = await repo.UpdateCategory(categoryDto);

            // Assert
            Assert.True(result); //ccessful

            
            var updatedCategory = await _dbContext.Categories.FindAsync(categoryDto.Id);
            Assert.NotNull(updatedCategory);
           
        }

    }
}
