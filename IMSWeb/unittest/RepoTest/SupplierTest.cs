using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Models;
using IMSWeb.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unittest.RepoTest
{
    public class SupplierTest
    {
        DbContextOptions<IMSContext> options;
        IMSContext _dbContext;

        public SupplierTest()
        {
            options = new DbContextOptionsBuilder<IMSContext>()
                .UseInMemoryDatabase(databaseName: "dummydb")
                .Options;

            _dbContext = new IMSContext(options);

            _dbContext.Suppliers.AddRange(
                new Supplier { Id = 1, Name = "Test", Email = "test@", Phone = "0097" },
                new Supplier { Id = 2, Name = "Test2", Email = "test2@", Phone = "0098" },
                new Supplier { Id = 3, Name = "Test23", Email = "test2@3", Phone = "33333" },
                new Supplier { Id = 4, Name = "Test23", Email = "test2@3", Phone = "33333" }
            );

            _dbContext.SaveChanges();
        }


        [Fact]
        public async Task getallsupplier()
        {


            SupplierRepo repo = new SupplierRepo(_dbContext);



            var res = await repo.GetSuppliers();
            var actual = 4;

            Assert.Equal(actual, res.Count);

            #region
            // Arrange - varable creation 
            // Act - call method
            //Assert - verify i get the right result
            #endregion
        }

        [Fact]
        public async Task getallsupplierbyid_exsist()
        {



            SupplierRepo repo = new SupplierRepo(_dbContext);



            var res = await repo.GetSupplierById(2);
            

           Assert.Equal(2, res.Id);

       
        }
        [Fact]
        public async Task Deletesupplier_exsist()
        {


            SupplierRepo repo = new SupplierRepo(_dbContext);

            var res = await repo.DeleteSupplier(2);
            //var res1 = await repo.Deletesuppplier(4);


            Assert.True(res);
            //Assert.True(!res1);

           
        }
        [Fact]
        public async Task UpdateSupplier_Exists()
        {
            // Arrange
            SupplierRepo repo = new SupplierRepo(_dbContext);
            var supplierDto = new SupplierDTO
            {
                Id = 1,
                Name = "Updated Supplier",
                Email = "updated@example.com",
                Phone = "999999999"
            };

            // Act
            var result = await repo.UpdateSupplier(supplierDto.Id, supplierDto);

            // Assert
            Assert.True(result); //uccessful

           
            var updatedSupplier = await _dbContext.Suppliers.FindAsync(supplierDto.Id);
            Assert.NotNull(updatedSupplier);
        }

        [Fact]
        public async Task createsupplier_exsist()
        {


            SupplierRepo repo = new SupplierRepo(_dbContext);

            var res = new SupplierDTO()
            {
                Name = "Test",
                Id = 6,
                Email ="mdk",
                Phone ="9884"
            };
            var resf = await repo.CreateSupplier(res);



            Assert.NotNull(resf);
        }

    }

}

