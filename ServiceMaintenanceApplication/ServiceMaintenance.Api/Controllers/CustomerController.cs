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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServiceRespository customerServiceRespository;

        public CustomerController(ICustomerServiceRespository customerServiceRespository)
        {
            this.customerServiceRespository = customerServiceRespository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetReport()
        {
            try
            {
                var customers = await customerServiceRespository.GetReport();
                return Ok(customers.ToList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetReport(int id)
        {
            try
            {
                var customer = await customerServiceRespository.GetReport(id);

                if (customer == null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddReport(Customer customer)
        {
            try
            {
                if (customer == null)
                    return BadRequest();

                var createdCustomer = await customerServiceRespository.AddReport(customer);

                return CreatedAtAction(nameof(GetReport),
                    new { id = createdCustomer.ID }, createdCustomer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new customer record");
            }
        }

        [HttpPut()]
        public async Task<ActionResult<Customer>> UpdateReport(Customer customer)
        {
            try
            {
                var customerToUpdate = await customerServiceRespository.GetReport(customer.ID);

                if (customerToUpdate == null)
                {
                    return NotFound($"Customer with Id = {customer.ID} not found");
                }

                var updatedCustomer = await customerServiceRespository.UpdateReport(customer);
                return Ok(updatedCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating data: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Customer>> DeleteReport(int id)
        {
            try
            {
                var customerToDelete = await customerServiceRespository.GetReport(id);

                if (customerToDelete == null)
                {
                    return NotFound($"Customer with Id = {id} not found");
                }

                await customerServiceRespository.DeleteReport(id);
                return NoContent(); // NoContent indicates successful deletion
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
