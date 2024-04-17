using IMSWeb.Dto;
using IMSWeb.Models;

namespace IMSWeb.Interface
{
    public interface IInventoryItem
    {
        //public Task<List<InventoryItems>> GetAllInventoryItems();
        public Task<List<InventoryItemDto>> GetAllInventoryItems();
        //public Task<InventoryItems> GetInventoryItemById(int id);
        public Task<InventoryItemDto> GetInventoryItemById(int id);
        //public Task<InventoryItems> GetInventoryItemByName(string name);
        public Task<InventoryItemDto> GetInventoryItemByName(string name);
        //public Task<bool> CreateInventoryItem(InventoryItems inventoryItem);
        public Task<bool> CreateInventoryItem(InventoryItemDto inventoryItemDto);

        public  Task<bool> CreateInventoryItemWithRelations(InventoryItemDto inventoryItemDto);
        public Task<bool> UpdateInventoryItemWithRelations(int id, InventoryItemDto inventoryItemDto);
        public Task<bool> DeleteInventoryItem(int id);
    }
}
