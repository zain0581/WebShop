using IMSWeb.Interface;
using IMSWeb.Models;
using IMSWeb.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IMSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrder OrderRepo { get; set; }
        public OrderController(IOrder orderRepo) 
        {
            OrderRepo = orderRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
           var res =  await OrderRepo.GetAllOrders();
            return Ok(res);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            bool result = await OrderRepo.CreateOrder(order);
            if (result)
            {
                return Ok("Order created successfully");
            }
            else
            {
                return StatusCode(500, "Failed to create Order");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await OrderRepo.DeleteOrder(id);
            if (order)
            {
                return Ok("Order Deleted successfully");
            }
            return StatusCode(500, "Ordre findes ikke");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder()
        {
            return Ok();
        }



        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await OrderRepo.GetOrderById(id);
            if (order == null)
            {
                return BadRequest();
            }
            return Ok(order);
        }


    


        
    }
}
