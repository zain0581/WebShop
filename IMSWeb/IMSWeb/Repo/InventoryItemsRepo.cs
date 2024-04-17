﻿using IMSWeb.Dal;
using IMSWeb.Interface;
using IMSWeb.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<InventoryItems>> GetAllInventoryItems()
        {
            return await _context.InventoryItems.ToListAsync();
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
