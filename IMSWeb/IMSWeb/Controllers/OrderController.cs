using IMSWeb.Dto;
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
        //public ICustomercs CustomerRepo { get; set; }
        //public IOrderItem OrderItemRepo { get; set; }
        public OrderController(IOrder orderRepo, ICustomercs customerRepo, IOrderItem orderItemRepo) 
        {
            //CustomerRepo = customerRepo;
            //OrderItemRepo = orderItemRepo;
            OrderRepo = orderRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
           var res =  await OrderRepo.GetAllOrders();
            return Ok(res);
        }


        //[HttpPost("new")]
        //public async Task<IActionResult> CreateOrder(OrderDto orderdto)
        //{
        //    Customer customer = new Customer
        //    {
        //        Name = orderdto.Customer.Name,
        //        Email = orderdto.Customer.Email,
        //        Phone = orderdto.Customer.Phone
        //    };
        //    await CustomerRepo.CreateCustomer(customer);

        //    Order order = new Order
        //    {
        //        OrderNo = orderdto.OrderNo,
        //        OrderDate = orderdto.OrderDate,
        //        GrossPrice = orderdto.GrossPrice,
        //        TotalPrice = orderdto.TotalPrice,
        //        Tax = orderdto.Tax

        //    };
        //    await OrderRepo.CreateOrder(order);



        //    return Ok(orderdto);

        //}


        //[HttpPost]
        //public async Task<IActionResult> cretorderandcustomer()
        //{

        //}

            [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            bool result = await OrderRepo.CreateOrder(orderDto);
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
