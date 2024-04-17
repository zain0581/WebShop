using IMSWeb.Dto;
using IMSWeb.Models;

namespace IMSWeb.Interface
{
    public interface ICategory
    {
        public Task<List<Category>> GetAllCategories();

        public Task<List<Category>> GetAllCategoriesWithInventoryItems();

        public Task<CategoryDto> GetCategoryByIdWithItem(int id);
        //public Task<Category> GetCategoryByIdd(int id);
        public Task<CategoryDto> GetCategoryByName(string name);
        public  Task<bool> CreateCategory(CategoryDto categoryDto);
        //public Task<bool> CreateCategory(Category category);
        //public Task<bool> UpdateCategory(int id, Category category);
        public Task<bool> UpdateCategory(CategoryUpdateDto categoryDto);
        public Task<bool> DeleteCategory(int id);
    }
}
