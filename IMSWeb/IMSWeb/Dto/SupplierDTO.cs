using System.Text.Json.Serialization;

namespace IMSWeb.Dto
{
    public class SupplierDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }

        [JsonIgnore]
        public List<InventoryItemDto>? InventoryItems { get; set; }
    }
}
