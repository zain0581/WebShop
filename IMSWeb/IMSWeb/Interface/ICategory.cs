using IMSWeb.Dto;
using IMSWeb.Models;

namespace IMSWeb.Interface
{
    public interface ICategory
    {
        public Task<List<CategoryDto>> GetAllCategories();

        public Task<List<Category>> GetAllCategoriesWithInventoryItems();

        public Task<Category> GetCategoryByIdWithItem(int id);
        //public Task<Category> GetCategoryByIdd(int id);
        public Task<Category> GetCategoryByName(string name);
        public  Task<bool> CreateCategory(CategoryDto categoryDto);
        //public Task<bool> CreateCategory(Category category);
        //public Task<bool> UpdateCategory(int id, Category category);
        public Task<bool> UpdateCategory(CategoryUpdateDto categoryDto);
        public Task<bool> DeleteCategory(int id);
    }
}
