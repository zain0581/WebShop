using IMSWeb.Dal;
using IMSWeb.Models;
using IMSWeb.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unittest.RepoTest
{
    public class CategoryTest
    {
        DbContextOptions<DbContext> options;
        IMSContext _dbContext;
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

        public void getallcategory()
        {


            CategoryRepo repo = new CategoryRepo(_dbContext);


            #region
            // Arrange - varable creation 
            // Act - call method
            //Assert - verify i get the right result
            #endregion
        }



    }
}
