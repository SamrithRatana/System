using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Data;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ServiceMaintenanceContext _context;

    public UsersController(ServiceMaintenanceContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetUsers()
    {
        var users = await _context.Users.Select(u => u.UserName).ToListAsync();
        return Ok(users);
    }
}
