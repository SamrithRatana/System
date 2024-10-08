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
    public class ReportDataController : ControllerBase
    {
        private readonly IServiceReportDataRespository ServiceReportRepository;

        public ReportDataController(IServiceReportDataRespository ServiceReportRepository)
        {
            this.ServiceReportRepository = ServiceReportRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceReportData>>> GetReport()
        {
            try
            {
                return (await ServiceReportRepository.GetReport()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceReportData>> GetReport(int id)
        {
            try
            {
                var result = await ServiceReportRepository.GetReport(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework)
                Console.WriteLine($"Error retrieving data: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }


        [HttpPost]
        public async Task<ActionResult<ServiceReportData>> AddReport(ServiceReportData serviceReport)
        {
            try
            {
                /* if (employee == null)
                     return BadRequest();

                 var emp = employeeRepository.ValidateEmployeeByEmail(employee.Email);

                 if (emp != null)
                 {
                     ModelState.AddModelError("email", "Employee email already in use");
                     return BadRequest(ModelState);
                 }*/
               
                var createdEmployee = await ServiceReportRepository.AddReport(serviceReport);

                return CreatedAtAction(nameof(GetReport),
                    new { id = createdEmployee.ID }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }
        [HttpPut()]
        public async Task<ActionResult<ServiceReportData>> UpdateReport(ServiceReportData serviceReport)
        {
            try
            {
                var reportToUpdate = await ServiceReportRepository.GetReport(serviceReport.ID);

                if (reportToUpdate == null)
                {
                    return NotFound($"Employee with Id = {serviceReport.ID} not found");
                }

                return await ServiceReportRepository.UpdateReport(serviceReport);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating data: {ex.Message}");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServiceReportData>> DeleteReport(int id)
        {
            try
            {
                var reportToDelete = await ServiceReportRepository.GetReport(id);

                if (reportToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                return await ServiceReportRepository.DeleteReport(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

    }
}
