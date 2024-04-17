using IMSWeb.Models;

namespace IMSWeb.Interface
{
    public interface IInventoryItem
    {
       public Task<List<InventoryItems>> GetAllInventoryItems();
        public Task<InventoryItems> GetInventoryItemById(int id);
        public Task<InventoryItems> GetInventoryItemByName(string name);
        public Task<bool> CreateInventoryItem(InventoryItems inventoryItem);
        public Task<bool> UpdateInventoryItem(int id, InventoryItems inventoryItem);
        public Task<bool> DeleteInventoryItem(int id);
    }
}
