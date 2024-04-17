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
        public async Task<IActionResult> CreateInventoryItems(InventoryItems items)
        {
            bool result = await InventoryItemRepo.CreateInventoryItem(items);
            if (!result)
            {
                return NotFound();
            }
            return Ok(items);
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

        [HttpPut]
        public async Task<IActionResult> UpdateInventoryItem(int id,[FromBody] InventoryItems inventoryItems)
        {
            var updateditemes = await InventoryItemRepo.UpdateInventoryItem(id, inventoryItems);
            if (!updateditemes)
            {
                return NotFound();
            }
            return Ok(updateditemes);
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
