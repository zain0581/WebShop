using System.Text.Json.Serialization;

namespace IMSWeb.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        public string? ImageUrl { get; set; }

        public int? IsActive { get; set; }

        // one to many reltion category can have many inventtorty items 
  
        public List<InventoryItems>? InventoryItems { get; set; }

    }
}
