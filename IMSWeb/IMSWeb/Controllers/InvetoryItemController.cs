using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Interface;
using IMSWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvetoryItemController : ControllerBase
    {
        public IInventoryItem InventoryItemRepo { get; set; }

        public InvetoryItemController(IInventoryItem inventoryItem)
        {
            InventoryItemRepo = inventoryItem;
          
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventoryItems()
        {
            var items = await InventoryItemRepo.GetAllInventoryItems();
            if (items == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(items);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventoryItems([FromBody] InventoryItemDto inventoryItemDto)
        {
            bool result = await InventoryItemRepo.CreateInventoryItem(inventoryItemDto);

            if (!result)
            {
                return NotFound();
            }

            return Ok(inventoryItemDto);
        }


        [HttpPost("CreateInventoryItemWithRelations")]
        public async Task<IActionResult> CreateInventoryItemWithRelations([FromBody] InventoryItemDto inventoryItemDto)
        {
            bool result = await InventoryItemRepo.CreateInventoryItemWithRelations(inventoryItemDto);

            if (!result)
            {
                return NotFound();
            }

            return Ok(inventoryItemDto);
        }





        [HttpDelete]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var items = await InventoryItemRepo.DeleteInventoryItem(id);
            if (!items)
            {
                return NotFound();
            }
            return Ok("InventoryItem deleted successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryItem(int id, [FromBody] InventoryItemDto inventoryItemDto)
        {
            var updated = await InventoryItemRepo.UpdateInventoryItemWithRelations(id, inventoryItemDto);
            if (!updated)
            {
                return NotFound();
            }
            return Ok(await InventoryItemRepo.GetInventoryItemById(id));
        }


        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetInventoryItemById(int id)
        {
            var items = await  InventoryItemRepo.GetInventoryItemById(id);
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }


        [HttpGet("name/{name}")]

        public async Task<IActionResult> GetInventoryItemByName(string name)
        {
         var items = await  InventoryItemRepo.GetInventoryItemByName(name);
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }
    }

    
}
