using IMSWeb.Dto;
using IMSWeb.Models;

namespace IMSWeb.Interface
{
    public interface IInventoryItem
    {

      public  Task<InventoryItems> CreateInventoryItem(InventoryItems inventoryItem);


        public Task<InventoryItems> CreateInventory(InventoryItems item);
        public Task<List<CreateInventoryItemRequest>> GetAllInventoryItems();
      
        public Task<InventoryItemDto> GetInventoryItemById(int id);
     
        public Task<InventoryItemDto> GetInventoryItemByName(string name);
        public Task<bool> UpdateInventoryItemWithRelations(int id, CreateInventoryItemRequest inventoryItemDto);
        public Task<bool> DeleteInventoryItem(int id);
    }
}
