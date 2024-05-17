using IMSWeb.Dto;
using IMSWeb.Interface;
using IMSWeb.Models;
using IMSWeb.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        public IOrderItem _orderItemRepository { get; set; }
        public IOrder orderRepo { get; set; }
        public IInventoryItem inventoryItemRepo { get; set; }



        public OrderItemController(IOrderItem orderItem, IOrder order, IInventoryItem inventoryItem) 
        {
            inventoryItemRepo = inventoryItem;
            orderRepo = order;
            this._orderItemRepository = orderItem;
        }


        [HttpPost("newcreate")]
        public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemRequest request)
        {
            // Retrieve order by its ID
            var order = await orderRepo.GetOrderById(request.Order.Id);
            if (order == null)
            {
                return BadRequest("Invalid order ID");
            }

            // Retrieve inventory item by its ID
            var inventoryItem = await inventoryItemRepo.GetInventoryItemById(request.InventoryItem.Id);
            if (inventoryItem == null)
            {
                return BadRequest("Invalid inventory item ID");
            }

            // Create the order item entity
            var orderItemEntity = new OrderItem
            {
                Quantity = request.Quantity,
                Price = request.Price,
                Order = order,
                //InventoryItem = inventoryItem,
            };

            // Use the order item repository to create the order item
            var createdOrderItem = await _orderItemRepository.CreateOrderItem(orderItemEntity);

            // Prepare the response DTO
            var response = new CreateOrderItemRequest
            {
                Id = createdOrderItem.Id,
                Quantity = createdOrderItem.Quantity,
                Price = createdOrderItem.Price,
                Order = new OrderDTOO
                {
                    Tax = order.Tax,    
                    Description = order.Description,
                    GrossPrice = order.GrossPrice,
                    OrderNo = order.OrderNo,    
                    TotalPrice = order.TotalPrice,
                    OrderDate = order.OrderDate,    
                    Id = createdOrderItem.Id,   
                    
                },
                InventoryItem = new InventoryItemDTOO
                {
                    Id = inventoryItem.Id,
                    Description = inventoryItem.Description,
                    ImageUrl = inventoryItem.ImageUrl,
                    Qty = inventoryItem.Qty,
                    Name   = inventoryItem.Name
                    
                    
                }
            };

            return Ok(response);
        }



        [HttpGet]
        public async Task<ActionResult<List<OrderItem>>> GetAllOrderItems()
        {
            var orderItems = await _orderItemRepository.GetAllOrderItems();
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItemById(int id)
        {
            var orderItem = await _orderItemRepository.GetOrderItemById(id);
            if (orderItem == null)
                return NotFound();

            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItemDto orderItem)
        {
            var result = await _orderItemRepository.CreateOrderItem(orderItem);
            if (!result)
                return BadRequest();

            return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.Id }, orderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.Id)
                return BadRequest();

            var result = await _orderItemRepository.UpdateOrderItem(orderItem);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var result = await _orderItemRepository.DeleteOrderItem(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
