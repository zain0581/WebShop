using IMSWeb.Dal;
using IMSWeb.Interface;
using IMSWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IMSContext _context;
        public TestController(IMSContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> hellodb()
        {
            await _context.Categories.AddAsync(new Category()
            {
                ImageUrl ="gh",
                IsActive= 1,
                Name = "Test",
                Description = "Test",

            });

            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("dd")]
        public async Task<IActionResult> addsupplier()
        {
            await _context.Suppliers.AddAsync(new Supplier()
            {
                Phone ="998",
                Name = "Test",
                Email = "Test",


            });

            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("ds")]
        public async Task<IActionResult> additem()
        {
            await _context.InventoryItems.AddAsync(new InventoryItems()
            {

                Name = "Test",
                Description = "Test",
                ImageUrl ="bd",
                IsAvailable=0,
               
              Category = _context.Categories.Find(1),
              Suppliers = _context.Suppliers.Find(1),


            });

            await _context.SaveChangesAsync();
            return Ok();
        }



        /////////////////////////////////////////////
        ///




        [HttpGet("customer")]
        public async Task<IActionResult>makecustomer()
        {
            await _context.Customers.AddAsync(new Customer()
            {
                Email = "Test",
                Name = "Test",
                Phone = "Test",

            });

            await _context.SaveChangesAsync();
            return Ok();
        }



        [HttpGet("order")]
        public async Task<IActionResult> makeorder()
        {
            await _context.Orders.AddAsync(new Order()
            {
                Description = "Test",
                GrossPrice= 10,
                OrderDate = DateTime.Now,
                OrderNo= "10",
                TotalPrice= 15,
                Tax=4,
                Customer =_context.Customers.Find(1)
                

            });

            await _context.SaveChangesAsync();
            return Ok();
        }



        [HttpGet("orderitem")]
        public async Task<IActionResult> makeorderitem()
        {
            await _context.OrderItems.AddAsync(new OrderItem()
            {

                Price= 10,
                Quantity= 10,
                InventoryItem = _context.InventoryItems.Find(1),
                Order= _context.Orders.Find(1),


            });

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
