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
    public class PrinterModelController : ControllerBase
    {
        private readonly IPrinterModelServiceRespository _printerModelRepository;

        public PrinterModelController(IPrinterModelServiceRespository printerModelRepository)
        {
            _printerModelRepository = printerModelRepository;
        }

        /* [HttpGet]
         public async Task<ActionResult<IEnumerable<PrinterModel>>> GetData()
         {
             try
             {
                 var printerModels = await _printerModelRepository.GetData();

                 // Define the base URL for image paths
                 var baseUrl = "https://localhost:7060";

                 // Construct the full URL for each image
                 foreach (var printerModel in printerModels)
                 {
                     if (!string.IsNullOrEmpty(printerModel.Photo))
                     {
                         printerModel.Photo = $"{baseUrl}/{printerModel.Photo}";
                     }
                 }

                 return Ok(printerModels.ToList());
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Error retrieving data: {ex.Message}");
                 return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
             }
         }*/
       /* [HttpGet("{id:int}")]
        public async Task<ActionResult<PrinterModel>> GetData(int id)
        {
            try
            {
                var result = await _printerModelRepository.GetData(id);
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
        }*/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrinterModel>>> GetData()
        {
            try
            {
                var printerModels = await _printerModelRepository.GetData();

               

                // Construct the full URL for each image
                foreach (var printerModel in printerModels)
                {
                    if (!string.IsNullOrEmpty(printerModel.Photo))
                    {
                        printerModel.Photo = $"{printerModel.Photo}";
                    }
                }

                return Ok(printerModels.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving data: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PrinterModel>> GetData(int id)
        {
            try
            {
                var result = await _printerModelRepository.GetData(id);
                if (result == null) return NotFound();

                // Construct the full URL for the image
                if (!string.IsNullOrEmpty(result.Photo))
                {
                   
                    result.Photo = $"{result.Photo}";
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
        public async Task<ActionResult<PrinterModel>> AddData(PrinterModel printerModel)
        {
            try
            {
                var createdPrinterModel = await _printerModelRepository.AddData(printerModel);

                return CreatedAtAction(nameof(GetData),
                    new { id = createdPrinterModel.PrinterModelID }, createdPrinterModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new printer model record");
            }
        }

        [HttpPut]
        public async Task<ActionResult<PrinterModel>> UpdateData(PrinterModel printerModel)
        {
            try
            {
                var printerModelToUpdate = await _printerModelRepository.GetData(printerModel.PrinterModelID);

                if (printerModelToUpdate == null)
                {
                    return NotFound($"PrinterModel with Id = {printerModel.PrinterModelID} not found");
                }

                return await _printerModelRepository.UpdateData(printerModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating data: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PrinterModel>> DeleteData(int id)
        {
            try
            {
                var printerModelToDelete = await _printerModelRepository.GetData(id);

                if (printerModelToDelete == null)
                {
                    return NotFound($"PrinterModel with Id = {id} not found");
                }

                return await _printerModelRepository.DeleteData(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
