using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceMaintenance.Api.Models;
using ServiceMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMaintenance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServiceRespository _categoryRepository;

        public CategoryController(ICategoryServiceRespository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetData()
        {
            try
            {
                return (await _categoryRepository.GetData()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetData(int id)
        {
            try
            {
                var result = await _categoryRepository.GetData(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error retrieving data: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddData(Category category)
        {
            try
            {
                var createdCategory = await _categoryRepository.AddData(category);

                return CreatedAtAction(nameof(GetData),
                    new { id = createdCategory.CategoryID }, createdCategory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new category record");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Category>> UpdateData(Category category)
        {
            try
            {
                var categoryToUpdate = await _categoryRepository.GetData(category.CategoryID);

                if (categoryToUpdate == null)
                {
                    return NotFound($"Category with Id = {category.CategoryID} not found");
                }

                return await _categoryRepository.UpdateData(category);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating data: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Category>> DeleteData(int id)
        {
            try
            {
                var categoryToDelete = await _categoryRepository.GetData(id);

                if (categoryToDelete == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }

                return await _categoryRepository.DeleteData(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
