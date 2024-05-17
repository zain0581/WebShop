using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Interface;
using IMSWeb.Models;
using IMSWeb.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IMSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvetoryItemController : ControllerBase
    {
        public IInventoryItem InventoryItemRepo { get; set; }
        public ICategory categoryrepo { get; set; }

        public ISupplier supplierrepo { get; set; }

        public InvetoryItemController(IInventoryItem inventoryItem, ICategory category, ISupplier supplier)
        {


            InventoryItemRepo = inventoryItem;
            categoryrepo = category;
            supplierrepo = supplier;

        }

        [HttpPost("newcreate")]
        public async Task<IActionResult> CreateInventoryItem([FromBody] CreateInventoryItemRequest request)
        {
            // Check if request or request.Category is null
            if (request == null || request.Category == null)
            {
                return BadRequest("Invalid request or category");
            }

            // Retrieve category by its ID
            var category = await categoryrepo.GetCategoryByIdWithItem(request.Category.Id);
            if (category == null)
            {
                return BadRequest("Invalid category ID");
            }

            // Check if request or request.Suppliers is null
            if (request.Suppliers == null)
            {
                return BadRequest("Invalid request or supplier");
            }

            // Retrieve supplier by its ID
            var supplier = await supplierrepo.GetSupplierById(request.Suppliers.Id);
            if (supplier == null)
            {
                return BadRequest("Invalid supplier ID");
            }

            // Create the inventory item entity
            var inventoryItemEntity = new InventoryItems
            {
                Name = request.Name,
                Description = request.Description,
                Qty = request.Qty,
                ImageUrl = request.ImageUrl,
                UnitPrice = request.UnitPrice,
                Category = category,
                Suppliers = supplier
            };

            // Use the inventory item repository to create the inventory item
            var createdInventoryItem = await InventoryItemRepo.CreateInventoryItem(inventoryItemEntity);

            // Prepare the response DTO
            var response = new CreateInventoryItemRequest
            {
                Id = createdInventoryItem.Id,
                Name = createdInventoryItem.Name,
                Description = createdInventoryItem.Description,
                Qty = createdInventoryItem.Qty,
                UnitPrice=createdInventoryItem.UnitPrice,
                ImageUrl = createdInventoryItem.ImageUrl,
                Category = new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ImageUrl = category.ImageUrl,
                    IsActive = category.IsActive
                },
                Suppliers = new SupplierDTO()
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Email = supplier.Email,
                    Phone = supplier.Phone
                }
            };

            return Ok(response);
        }


        //[HttpPost("newcreate")]
        //    public async Task<IActionResult> CreateInventoryItem([FromBody] CreateInventoryItemRequest request)
        //    {
        //        // Retrieve category by its ID
        //        var category = await categoryrepo.GetCategoryByIdWithItem(request.Category.Id);
        //        if (category == null)
        //        {
        //            return BadRequest("Invalid category ID");
        //        }

        //        // Retrieve supplier by its ID
        //        var supplier = await supplierrepo.GetSupplierById(request.Suppliers.Id);
        //        if (supplier == null)
        //        {
        //            return BadRequest("Invalid supplier ID");
        //        }

        //        // Create the inventory item entity
        //        var inventoryItemEntity = new InventoryItems
        //        {
        //            Name = request.Name,
        //            Description = request.Description,
        //            IsAvailable = request.IsAvailable,
        //            ImageUrl = request.ImageUrl,
        //            Category = category,
        //            Suppliers= supplier

        //        };

        //        // Use the inventory item repository to create the inventory item
        //        var createdInventoryItem = await InventoryItemRepo.CreateInventoryItem(inventoryItemEntity);

        //        // Prepare the response DTO
        //        var response = new CreateInventoryItemRequest
        //        {
        //            Id = createdInventoryItem.Id,
        //            Name = createdInventoryItem.Name,
        //            Description = createdInventoryItem.Description,
        //            IsAvailable = createdInventoryItem.IsAvailable,
        //            ImageUrl = createdInventoryItem.ImageUrl,
        //            Category = new CategoryDto()
        //            {
        //                Id = category.Id,
        //                Name = category.Name,
        //                Description = category.Description,
        //                ImageUrl = category.ImageUrl,
        //                IsActive = category.IsActive
        //            },
        //            Suppliers= new SupplierDTO()
        //            {
        //                Id= supplier.Id,    
        //                Name = supplier.Name,   
        //                Email = supplier.Email, 
        //                Phone = supplier.Phone
        //            }





        //        };

        //            return Ok(response);
        //        }


        [HttpPost("IvnOnly")]
        public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryItemRequest request)
        {

            var inventoryItemEntity = new InventoryItems
            {
                Name = request.Name,
                Description = request.Description,
                Qty = request.Qty,
                ImageUrl = request.ImageUrl,
                //Category = request.Category,
            };
            var createdInventoryItem = await InventoryItemRepo.CreateInventoryItem(inventoryItemEntity);

            var response = new CreateInventoryItemRequest
            {
                Id = createdInventoryItem.Id,
                Name = createdInventoryItem.Name,
                Description = createdInventoryItem.Description,
                Qty = createdInventoryItem.Qty,
                ImageUrl = createdInventoryItem.ImageUrl,

            };
            return Ok(response);
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

        //[HttpPost]
        //public async Task<IActionResult> CreateInventoryItems([FromBody] InventoryItemDto inventoryItemDto)
        //{




        //    bool result = await InventoryItemRepo.CreateInventoryItem(inventoryItemDto);

        //    if (!result)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(inventoryItemDto);
        //}


        //[HttpPost("CreateInventoryItemWithRelations")]
        //public async Task<IActionResult> CreateInventoryItemWithRelations([FromBody] InventoryItemDto inventoryItemDto)
        //{
        //    bool result = await InventoryItemRepo.CreateInventoryItemWithRelations(inventoryItemDto);

        //    if (!result)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(inventoryItemDto);
        //}





        [HttpDelete("{id}")]
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
        public async Task<IActionResult> UpdateInventoryItem(int id, [FromBody] CreateInventoryItemRequest inventoryItemDto)
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
