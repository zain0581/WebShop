using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Interface;
using IMSWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace IMSWeb.Repo
{
    public class InventoryItemsRepo : IInventoryItem
    {
        private IMSContext _context;

        public InventoryItemsRepo(IMSContext context)
        {
            _context = context;

            //var datalist = from p in _context.InventoryItems
            //               on 
        }

        //public async Task<InventoryItems> CreateInventoryItem(InventoryItems inventoryItem)
        //{
        //    _context.InventoryItems.Add(inventoryItem);
        //    await _context.SaveChangesAsync();
        //    return inventoryItem;
        //}

        public async Task<InventoryItems> CreateInventoryItem(InventoryItems item)
        {
            // Detach the category entity if it's being tracked
            if (_context.Entry(item).State == EntityState.Detached)
            {
                _context.Entry(item).State = EntityState.Modified; // Or EntityState.Added if it's a new entity
            }


            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }


        public async Task<InventoryItems> CreateInventory(InventoryItems item)
        {

            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }











        public async Task<bool> DeleteInventoryItem(int id)
        {
            var item = await _context.InventoryItems.Include(x => x.Category).Include(g => g.Suppliers).FirstOrDefaultAsync(c => c.Id == id);
            if (item == null)
                return false;

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CreateInventoryItemRequest>> GetAllInventoryItems()
        {
            var inventoryItems = await _context.InventoryItems
                .Include(i => i.Category)
                .Include(i => i.Suppliers)
                .ToListAsync();

            var inventoryItemDTOs = inventoryItems.Select(i => new CreateInventoryItemRequest
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                IsAvailable = i.IsAvailable,
                ImageUrl = i.ImageUrl,
                Category = i.Category != null ? new CategoryDto
                {
                    Id = i.Category.Id,
                    Name = i.Category.Name,
                    Description = i.Category.Description,
                    IsActive = i.Category.IsActive,
                    ImageUrl = i.Category.ImageUrl
                    
                } : null, // Set CategoryDto to null if Category is null
                Suppliers = i.Suppliers != null ? new SupplierDTO
                {
                    Id = i.Suppliers.Id,
                    Email = i.Suppliers.Email,
                    Name = i.Suppliers.Name,
                    Phone = i.Suppliers.Phone,

                    

                } : null // Set Suppliers to null if it's null
            }).ToList();

            return inventoryItemDTOs;
        }



        public async Task<InventoryItemDto> GetInventoryItemById(int id)
        {
            var inventoryItem = await _context.InventoryItems
                .Include(i => i.Category)
                .Include(i => i.Suppliers)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventoryItem == null)
            {
                return null;
            }

            // Map InventoryItem to InventoryItemDto
            var inventoryItemDto = new InventoryItemDto
            {
                Id = inventoryItem.Id,
                Name = inventoryItem.Name,
                Description = inventoryItem.Description,
                IsAvailable = inventoryItem.IsAvailable,
                ImageUrl = inventoryItem.ImageUrl,
                //Category = inventoryItem.Category != null ? new CategoryDto
                //{
                //    Id = inventoryItem.Category.Id,
                //    Name = inventoryItem.Category.Name,
                //    Description = inventoryItem.Category.Description,
                //    IsActive = inventoryItem.Category.IsActive,
                //    ImageUrl = inventoryItem.Category.ImageUrl
                //    // Map other category properties as needed
                //} : null, // Set CategoryDto to null if Category is null
                //Suppliers = inventoryItem.Suppliers != null ? inventoryItem.Suppliers.Select(s => new CreateSupplierDto
                //{
                //    Id = s.Id,
                //    Name = s.Name,
                //    Phone = s.Phone
                //    // Map other supplier properties as needed
                //}).ToList() : null // Set Suppliers to null if it's null
            };

            return inventoryItemDto;
        }


        public async Task<InventoryItemDto> GetInventoryItemByName(string name)
        {
            var inventoryItem = await _context.InventoryItems
                .Include(i => i.Category)
                .Include(i => i.Suppliers)
                .FirstOrDefaultAsync(i => i.Name == name);

            if (inventoryItem == null)
            {
                return null;
            }

            // Map InventoryItem to InventoryItemDto
            var inventoryItemDto = new InventoryItemDto
            {
                Id = inventoryItem.Id,
                Name = inventoryItem.Name,
                Description = inventoryItem.Description,
                IsAvailable = inventoryItem.IsAvailable,
                ImageUrl = inventoryItem.ImageUrl,
                //Category = inventoryItem.Category != null ? new CategoryDto
                //{
                //    Id = inventoryItem.Category.Id,
                //    Name = inventoryItem.Category.Name,
                //    Description = inventoryItem.Category.Description,
                //    IsActive = inventoryItem.Category.IsActive,
                //    ImageUrl = inventoryItem.Category.ImageUrl
                //    // Map other category properties as needed
                //} : null, // Set CategoryDto to null if Category is null
                //Suppliers = inventoryItem.Suppliers != null ? inventoryItem.Suppliers.Select(s => new CreateSupplierDto
                //{
                //    Id = s.Id,
                //    Name = s.Name,
                //    Phone = s.Phone
                //    // Map other supplier properties as needed
                //}).ToList() : null // Set Suppliers to null if it's null
            };

            return inventoryItemDto;
        }


        public async Task<bool> UpdateInventoryItemWithRelations(int id, CreateInventoryItemRequest inventoryItemDto)
        {
            // Retrieve the inventory item from the database including its related category and supplier
            var inventoryItem = await _context.InventoryItems
                .Include(i => i.Category)
                .Include(i => i.Suppliers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (inventoryItem == null)
            {
                return false; // If the inventory item with the specified id doesn't exist, return false
            }

            // Update the properties of the retrieved inventory item with the values from the DTO
            inventoryItem.Name = inventoryItemDto.Name;
            inventoryItem.Description = inventoryItemDto.Description;
            inventoryItem.IsAvailable = inventoryItemDto.IsAvailable;
            inventoryItem.ImageUrl = inventoryItemDto.ImageUrl;
           

            // Check if the category and supplier in the DTO are provided and update if necessary
            if (inventoryItemDto.Category != null)
            {
                // If the category is provided, update the inventory item's category
                inventoryItem.Category = new Category
                {
                    Id = inventoryItemDto.Category.Id,
                    Name = inventoryItemDto.Category.Name,
                    Description = inventoryItemDto.Category.Description,
                    ImageUrl = inventoryItemDto.Category.ImageUrl,
                    IsActive= inventoryItemDto.Category.IsActive,
                    // If isActive is a property of Category, update it accordingly
                    // isActive = inventoryItemDto.Category.isActive
                };
            }

            if (inventoryItemDto.Suppliers != null)
            {
                // If the supplier is provided, update the inventory item's supplier
                inventoryItem.Suppliers = new Supplier
                {
                    Id = inventoryItemDto.Suppliers.Id,
                    Name = inventoryItemDto.Suppliers.Name,
                    Email = inventoryItemDto.Suppliers.Email,
                    Phone = inventoryItemDto.Suppliers.Phone
                };
            }

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return true; // Return true indicating successful update
        }


    }
}