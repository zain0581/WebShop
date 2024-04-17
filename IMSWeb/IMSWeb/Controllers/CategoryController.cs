using IMSWeb.Dal;
using IMSWeb.Dto;
using IMSWeb.Interface;
using IMSWeb.Models;
using IMSWeb.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMSWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public ICategory CategoryRepo { get; set; }

        public CategoryController(ICategory category)
        {
            CategoryRepo = category;
        }


        //[HttpGet]
        //public async Task<IActionResult> GetAllCategory()
        //{
        //    var category = await CategoryRepo.GetAllCategories();
        //    return Ok(category);
        //}


        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesWithInventoryItems()
        {
            var categories = await CategoryRepo.GetAllCategoriesWithInventoryItems();
            return Ok(categories);
        }

      
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await CategoryRepo.CreateCategory(categoryDto);
            if (result)
            {
                return Ok("Category created successfully");
            }
            else
            {
                return StatusCode(500, "Failed to create category");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await CategoryRepo.DeleteCategory(id);
            if (result)
            {
                return Ok("Category deleted successfully");
            }
            else
            {
                return NotFound("Category not found");
            }
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await CategoryRepo.GetCategoryByIdWithItem(id);
            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return NotFound("Category not found");
            }

        }


        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            var category = await CategoryRepo.GetCategoryByName(name);
            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return NotFound("Category not found");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest("Category ID in the request body does not match the ID in the URL.");
            }

            bool result = await CategoryRepo.UpdateCategory(categoryDto);
            if (result)
            {
                return Ok("Category updated successfully");
            }
            else
            {
                return StatusCode(500, "Failed to update category");
            }
        }

    }
}
