namespace IMSWeb.Dto
{
    public class SupplierDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<InventoryItemDto>? InventoryItems { get; set; }
    }
}
