using Microsoft.AspNetCore.Mvc;
using ServiceMaintenance.Api.Models;
using ServiceMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ServiceMaintenance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerRespository _manufacturerRespository;

        public ManufacturerController(IManufacturerRespository manufacturerRespository)
        {
            _manufacturerRespository = manufacturerRespository;
        }

        // GET: api/manufacturer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> GetManufacturers()
        {
            try
            {
                var manufacturers = await _manufacturerRespository.GetData();
                return Ok(manufacturers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving data from the database: {ex.Message}");
            }
        }

        // GET: api/manufacturer/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Manufacturer>> GetManufacturer(int id)
        {
            try
            {
                var manufacturer = await _manufacturerRespository.GetData(id);

                if (manufacturer == null)
                    return NotFound($"Manufacturer with Id = {id} not found.");

                return Ok(manufacturer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving data from the database: {ex.Message}");
            }
        }

        // POST: api/manufacturer
        [HttpPost]
        public async Task<ActionResult<Manufacturer>> AddManufacturer(Manufacturer manufacturer)
        {
            try
            {
                if (manufacturer == null)
                    return BadRequest("Invalid manufacturer data.");

                var createdManufacturer = await _manufacturerRespository.AddData(manufacturer);

                return CreatedAtAction(nameof(GetManufacturer),
                    new { id = createdManufacturer.ManufacturerID }, createdManufacturer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error creating new manufacturer record: {ex.Message}");
            }
        }

        // PUT: api/manufacturer
        [HttpPut()]
        public async Task<ActionResult<Manufacturer>> UpdateManufacturer(Manufacturer manufacturer)
        {
            try
            {
                var manufacturerToUpdate = await _manufacturerRespository.GetData(manufacturer.ManufacturerID);

                if (manufacturerToUpdate == null)
                {
                    return NotFound($"Manufacturer with Id = {manufacturer.ManufacturerID} not found.");
                }

                return await _manufacturerRespository.UpdateData(manufacturer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating manufacturer data: {ex.Message}");
            }
        }

        // DELETE: api/manufacturer/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Manufacturer>> DeleteManufacturer(int id)
        {
            try
            {
                var manufacturerToDelete = await _manufacturerRespository.GetData(id);

                if (manufacturerToDelete == null)
                {
                    return NotFound($"Manufacturer with Id = {id} not found.");
                }

                return await _manufacturerRespository.DeleteData(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting manufacturer: {ex.Message}");
            }
        }
    }
}
