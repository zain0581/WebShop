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

        public InventoryItemsRepo(IMSContext context )
        {
            _context = context;
        }

        public async Task<bool> CreateInventoryItem(InventoryItems inventoryItem)
        {
           var items = _context.InventoryItems.Add( inventoryItem );
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteInventoryItem(int id)
        {
            var item = await _context.InventoryItems.FindAsync(id);
            if (item == null)
                return false;

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<InventoryItemDto>> GetAllInventoryItems()
        {
            var inventoryItems = await _context.InventoryItems
                .Include(i => i.Category)
                .Include(i => i.Suppliers)
                .ToListAsync();

            var inventoryItemDTOs = inventoryItems.Select(i => new InventoryItemDto
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
                    // Map other category properties as needed
                } : null, // Set CategoryDto to null if Category is null
                Suppliers = i.Suppliers != null ? i.Suppliers.Select(s => new SupplierDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone
                    // Map other supplier properties as needed
                }).ToList() : null // Set Suppliers to null if it's null
            }).ToList();

            return inventoryItemDTOs;
        }


        public async Task<InventoryItems> GetInventoryItemById(int id)
        {
           return  await _context.InventoryItems.FindAsync(id);   
        }

        public async Task<InventoryItems> GetInventoryItemByName(string name)
        {
           return await  _context.InventoryItems.FirstOrDefaultAsync(x => x.Name == name);   
        }

        public async Task<bool> UpdateInventoryItem(int id, InventoryItems inventoryItem)
        {
          var items = await  _context.InventoryItems.FirstOrDefaultAsync(x => x.Id == id);
            if(items == null)
            {
                return false;
            }
              
            items.Name=inventoryItem.Name;
          
            items.Description = inventoryItem.Description;
            items.ImageUrl = inventoryItem.ImageUrl;
            items.IsAvailable = inventoryItem.IsAvailable;
            items.CategoryId = inventoryItem.CategoryId;

            await _context.SaveChangesAsync();
            return true;

        }
    }
}
