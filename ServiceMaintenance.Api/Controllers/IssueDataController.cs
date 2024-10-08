using Microsoft.AspNetCore.Mvc;
using ServiceMaintenance.Api.Models;
using ServiceMaintenance.Models;

namespace ServiceMaintenance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueDataController : ControllerBase
    {
        private readonly IIssueDataRespository issueDataRespository;

        public IssueDataController(IIssueDataRespository issueDataRespository)
        {
            this.issueDataRespository = issueDataRespository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssue()
        {
            try
            {
                return (await issueDataRespository.GetIssue()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Issue>> GetIssue(int id)
        {
            try
            {
                var result = await issueDataRespository.GetIssue(id);

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
        public async Task<ActionResult<Issue>> AddIssue(Issue serviceReport)
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

                var createdIssue = await issueDataRespository.AddIssue(serviceReport);

                return CreatedAtAction(nameof(GetIssue),
                    new { id = createdIssue.IssueID }, createdIssue);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }
        [HttpPut()]
        public async Task<ActionResult<Issue>> UpdateIssue(Issue serviceReport)
        {
            try
            {
                var reportToUpdate = await issueDataRespository.GetIssue(serviceReport.IssueID);

                if (reportToUpdate == null)
                {
                    return NotFound($"Employee with Id = {serviceReport.IssueID} not found");
                }

                return await issueDataRespository.UpdateIssue(serviceReport);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating data: {ex.Message}");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Issue>> DeleteIssue(int id)
        {
            try
            {
                var reportToDelete = await issueDataRespository.GetIssue(id);

                if (reportToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                return await issueDataRespository.DeleteIssue(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }


    }
}
