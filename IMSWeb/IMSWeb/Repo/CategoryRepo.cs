﻿using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Interface;
using IMSWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace IMSWeb.Repo
{
    public class CategoryRepo : ICategory
    {
        private IMSContext _dbcontext;
        public CategoryRepo(IMSContext context)
        {
            _dbcontext = context;
        }

       
        
            public async Task<bool> CreateCategory(CategoryDto categoryDto)
            {
            var category = new Category
            {
                Id=categoryDto.Id,
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                ImageUrl = categoryDto.ImageUrl,
                IsActive = categoryDto.IsActive,

            };

            _dbcontext.Categories.Add(category);
            await _dbcontext.SaveChangesAsync();
            return true;
            }

        public async Task<bool> DeleteCategory(int id)
        {
            var categoryToDelete = await _dbcontext.Categories.Include(f => f.InventoryItems).FirstOrDefaultAsync(y => y.Id == id);
            if (categoryToDelete == null)
                return false;

            // Delete associated InventoryItems
            _dbcontext.InventoryItems.RemoveRange(categoryToDelete.InventoryItems);

            // Remove the category itself
            _dbcontext.Categories.Remove(categoryToDelete);

            await _dbcontext.SaveChangesAsync();

            // Return true to indicate successful deletion
            return true;
        }


        public async Task<List<CategoryDto>> GetAllCategories()
        {
            return await _dbcontext.Categories.Select(x=> new CategoryDto
            {
                Id = x.Id,
                Name=x.Name,
                Description=x.Description,
                IsActive=x.IsActive,
                ImageUrl=x.ImageUrl

            }).ToListAsync();
        }

        public async Task<List<Category>> GetAllCategoriesWithInventoryItems()
        {
            try
            {
                return await _dbcontext.Categories
                    .Include(c => c.InventoryItems)
                    .Select(c => new Category
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        ImageUrl = c.ImageUrl,
                        IsActive = c.IsActive,
                        InventoryItems = c.InventoryItems.Select(i => new InventoryItems
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Description = i.Description,
                            Qty = i.Qty,
                            ImageUrl = i.ImageUrl
                        }).ToList()


                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<Category> GetCategoryByIdWithItem(int id)
        {
            var category = await _dbcontext.Categories
                .Include(c => c.InventoryItems)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return null; // or thro
            }

            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageUrl = category.ImageUrl,
                IsActive = category.IsActive,
                InventoryItems = category.InventoryItems.Select(ii => new InventoryItems
                {
                    Id = ii.Id,
                    Name = ii.Name,
                    Description = ii.Description,
                    Qty = ii.Qty,
                    ImageUrl = ii.ImageUrl,
                    // Y properties as 
                }).ToList()
            };
        }
    

        public async Task<Category> GetCategoryByName(string name)
        {
            var category = await _dbcontext.Categories
                .Include(c => c.InventoryItems)
                .FirstOrDefaultAsync(c => c.Name == name);

            if (category == null)
            {
                return null; 
            }
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageUrl = category.ImageUrl,
                IsActive = category.IsActive,
                InventoryItems = category.InventoryItems.Select(ii => new InventoryItems
                {
                    Id = ii.Id,
                    Name = ii.Name,
                    Description = ii.Description,
                    Qty = ii.Qty,
                    ImageUrl = ii.ImageUrl,
                    // we can include
                }).ToList()
            };



        }


        public async Task<bool> UpdateCategory(CategoryDto categoryDto)
        {
            try
            {
                var existingCategory = await _dbcontext.Categories.FindAsync(categoryDto.Id);

                if (existingCategory == null)
                    return false; // Category not found

                // Update category properties
                existingCategory.Name = categoryDto.Name;
                existingCategory.Description = categoryDto.Description;
                existingCategory.ImageUrl = categoryDto.ImageUrl;
                existingCategory.IsActive = categoryDto.IsActive;

                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Log or handle the
                return false;
            }
        }


    }
}
