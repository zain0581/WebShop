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

        public async Task<bool> CreateInventoryItem(InventoryItemDto inventoryItemDto)
        {
            // Map InventoryItemDto to InventoryItem
            var inventoryItem = new InventoryItems
            {
                Name = inventoryItemDto.Name,
                Description = inventoryItemDto.Description,
                IsAvailable = inventoryItemDto.IsAvailable,
                ImageUrl = inventoryItemDto.ImageUrl
                // Map other properties as needed
            };

            // Add InventoryItem to context and save changes
            _context.InventoryItems.Add(inventoryItem);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> CreateInventoryItemWithRelations(InventoryItemDto inventoryItemDto)
        {
            // Map InventoryItemDto to InventoryItem
            var inventoryItem = new InventoryItems
            {
                Name = inventoryItemDto.Name,
                Description = inventoryItemDto.Description,
                IsAvailable = inventoryItemDto.IsAvailable,
                ImageUrl = inventoryItemDto.ImageUrl
                // Map other properties as needed
            };

            // Retrieve or create Category
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == inventoryItemDto.Category.Name);
            if (category == null)
            {
                category = new Category
                {
                    Name = inventoryItemDto.Category.Name,
                    Description = inventoryItemDto.Category.Description,
                    IsActive = inventoryItemDto.Category.IsActive,
                    ImageUrl = inventoryItemDto.Category.ImageUrl
                    // Map other Category properties as needed
                };
                _context.Categories.Add(category);
            }

            // Create Supplier(s) and associate with InventoryItem
            var suppliers = new List<Supplier>();
            foreach (var supplierDto in inventoryItemDto.Suppliers)
            {
                var supplier = new Supplier
                {
                    Name = supplierDto.Name,
                    Phone = supplierDto.Phone,
                    Email = supplierDto.Email,  
                    // Map other Supplier properties as needed
                };
                suppliers.Add(supplier);
            }

            // Associate Category and Supplier(s) with InventoryItem
            inventoryItem.Category = category;
            inventoryItem.Suppliers = suppliers;

            // Add InventoryItem to context and save changes
            _context.InventoryItems.Add(inventoryItem);
            await _context.SaveChangesAsync();

            return true;
        }




        public async Task<bool> DeleteInventoryItem(int id)
        {
            var item = await _context.InventoryItems.Include(x=>x.Category).Include(g=>g.Suppliers).FirstOrDefaultAsync(c=>c.Id==id);
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
                Suppliers = i.Suppliers != null ? i.Suppliers.Select(s => new CreateSupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone
                    // Map other supplier properties as needed
                }).ToList() : null // Set Suppliers to null if it's null
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
                Category = inventoryItem.Category != null ? new CategoryDto
                {
                    Id = inventoryItem.Category.Id,
                    Name = inventoryItem.Category.Name,
                    Description = inventoryItem.Category.Description,
                    IsActive = inventoryItem.Category.IsActive,
                    ImageUrl = inventoryItem.Category.ImageUrl
                    // Map other category properties as needed
                } : null, // Set CategoryDto to null if Category is null
                Suppliers = inventoryItem.Suppliers != null ? inventoryItem.Suppliers.Select(s => new CreateSupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone
                    // Map other supplier properties as needed
                }).ToList() : null // Set Suppliers to null if it's null
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
                Category = inventoryItem.Category != null ? new CategoryDto
                {
                    Id = inventoryItem.Category.Id,
                    Name = inventoryItem.Category.Name,
                    Description = inventoryItem.Category.Description,
                    IsActive = inventoryItem.Category.IsActive,
                    ImageUrl = inventoryItem.Category.ImageUrl
                    // Map other category properties as needed
                } : null, // Set CategoryDto to null if Category is null
                Suppliers = inventoryItem.Suppliers != null ? inventoryItem.Suppliers.Select(s => new CreateSupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone
                    // Map other supplier properties as needed
                }).ToList() : null // Set Suppliers to null if it's null
            };

            return inventoryItemDto;
        }


        public async Task<bool> UpdateInventoryItemWithRelations(int id, InventoryItemDto inventoryItemDto)
        {
            var inventoryItem = await _context.InventoryItems
                .Include(i => i.Category)
                .Include(i => i.Suppliers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (inventoryItem == null)
            {
                return false;
            }

            // Update inventory item properties
            inventoryItem.Name = inventoryItemDto.Name;
            inventoryItem.Description = inventoryItemDto.Description;
            inventoryItem.ImageUrl = inventoryItemDto.ImageUrl;
            inventoryItem.IsAvailable = inventoryItemDto.IsAvailable;

            // Update category if provided
            if (inventoryItemDto.Category != null)
            {
                inventoryItem.Category ??= new Category();
                inventoryItem.Category.Name = inventoryItemDto.Category.Name;
                inventoryItem.Category.Description = inventoryItemDto.Category.Description;
                inventoryItem.Category.IsActive = inventoryItemDto.Category.IsActive;
                inventoryItem.Category.ImageUrl = inventoryItemDto.Category.ImageUrl;
            }

            // Update suppliers if provided
            if (inventoryItemDto.Suppliers != null)
            {
                inventoryItem.Suppliers ??= new List<Supplier>();
                inventoryItem.Suppliers.Clear(); // Remove existing suppliers

                foreach (var supplierDto in inventoryItemDto.Suppliers)
                {
                    var supplier = await _context.Suppliers.FindAsync(supplierDto.Id);
                    if (supplier != null)
                    {
                        inventoryItem.Suppliers.Add(supplier);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
