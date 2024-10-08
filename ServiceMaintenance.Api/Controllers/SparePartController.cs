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
    public class SparePartController : ControllerBase
    {
        private readonly ISparePartServiceRespository _sparePartRepository;

        public SparePartController(ISparePartServiceRespository sparePartRepository)
        {
            _sparePartRepository = sparePartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SparePart>>> GetData()
        {
            try
            {
                var spareParts = await _sparePartRepository.GetData();

                // Define the base URL for image paths
                var baseUrl = "https://localhost:7060";

                // Construct the full URL for each image
                foreach (var sparePart in spareParts)
                {
                    if (!string.IsNullOrEmpty(sparePart.Photo))
                    {
                        sparePart.Photo = $"{baseUrl}/{sparePart.Photo}";
                    }
                }

                return Ok(spareParts.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving data: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<SparePart>> GetData(int id)
        {
            try
            {
                var result = await _sparePartRepository.GetData(id);
                if (result == null) return NotFound();

                // Construct the full URL for the image
                if (!string.IsNullOrEmpty(result.Photo))
                {
                    var baseUrl = "https://localhost:7060"; // Set the base URL here
                    result.Photo = $"{baseUrl}/{result.Photo}";
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving data: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }



        [HttpPost]
        public async Task<ActionResult<SparePart>> AddData(SparePart sparePart)
        {
            try
            {
                var createdSparePart = await _sparePartRepository.AddData(sparePart);

                return CreatedAtAction(nameof(GetData),
                    new { id = createdSparePart.SparePartID }, createdSparePart);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new spare part record");
            }
        }

        [HttpPut]
        public async Task<ActionResult<SparePart>> UpdateData(SparePart sparePart)
        {
            try
            {
                var sparePartToUpdate = await _sparePartRepository.GetData(sparePart.SparePartID);

                if (sparePartToUpdate == null)
                {
                    return NotFound($"SparePart with Id = {sparePart.SparePartID} not found");
                }

                return await _sparePartRepository.UpdateData(sparePart);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating data: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<SparePart>> DeleteData(int id)
        {
            try
            {
                var sparePartToDelete = await _sparePartRepository.GetData(id);

                if (sparePartToDelete == null)
                {
                    return NotFound($"SparePart with Id = {id} not found");
                }

                return await _sparePartRepository.DeleteData(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
