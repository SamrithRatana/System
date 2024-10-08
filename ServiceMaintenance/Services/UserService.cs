using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Data;
using ServiceMaintenance.Models;
using ServiceMaintenance.ViewModel;

public class UserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ServiceMaintenanceContext _dbContext; // Inject DbContext

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ServiceMaintenanceContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;

    }
    public async Task<ApplicationUser> GetApplicationUserAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }
    public async Task<List<UserViewModel>> GetAllUsersForChatAsync()
    {
        try
        {
            var users = await _userManager.Users
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    ProfilePicture = user.ProfilePicture,

                })
                .ToListAsync();

            return users;
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Error occurred: {ex.Message}");
            throw; // Or handle accordingly
        }
    }

    public async Task SaveMessageAsync(string userId, string recipientId, string messageText, string fileUrl)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        var message = new Message
        {
            UserName = user.UserName,
            Text = messageText,
            UserID = userId,
            RecipientID = recipientId,
            FileUrl = fileUrl
        };

        _dbContext.Messages.Add(message);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<List<Message>> GetMessagesAsync(string userId, string recipientId)
    {
        return await _dbContext.Messages
            .Where(m => (m.UserID == userId && m.RecipientID == recipientId) ||
                        (m.UserID == recipientId && m.RecipientID == userId))
            .OrderBy(m => m.When)
            .ToListAsync();
    }
    public async Task<List<UserViewModel>> GetUsersAsync()
    {
        var users = await _userManager.Users
            .Select(user => new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            })
            .ToListAsync();
        foreach (var user in users)
        {
            var userEntity = await _userManager.FindByIdAsync(user.Id);
            user.Roles = await _userManager.GetRolesAsync(userEntity);
        }

        return users;
    }

    public async Task<UserViewModel> GetUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return null;

        return new UserViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = await _userManager.GetRolesAsync(user)
        };
    }
    public async Task<List<CheckBoxViewModel>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return roles.Select(role => new CheckBoxViewModel
        {
            RoleId = role.Id,
            RoleName = role.Name,
            DisplayValue = role.Name,
            IsSelected = false // Default to unselected
        }).ToList();
    }

    public async Task<IdentityResult> CreateUserAsync(UserViewModel userModel)
    {
        var user = new ApplicationUser
        {
            UserName = userModel.UserName,
            Email = userModel.Email,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
        };
        // Create the user
        var result = await _userManager.CreateAsync(user, userModel.Password);

        if (result.Succeeded)
        {
            // Assign roles to the user
            if (userModel.Roles != null && userModel.Roles.Any())
            {
                await _userManager.AddToRolesAsync(user, userModel.Roles);
            }
        }

        return result;
    }
    public async Task<IdentityResult> UpdateUserAsync(UserViewModel userModel)
    {
        var user = await _userManager.FindByIdAsync(userModel.Id);
        if (user == null) return IdentityResult.Failed();

        user.FirstName = userModel.FirstName;
        user.LastName = userModel.LastName;
        user.Email = userModel.Email;
        user.UserName = userModel.UserName;


        return await _userManager.UpdateAsync(user);
    }
    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return IdentityResult.Failed();

        return await _userManager.DeleteAsync(user);
    }
    public async Task<UserRolesViewModel> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return null;
        }

        var allRoles = await _roleManager.Roles.ToListAsync();
        var userRoles = await _userManager.GetRolesAsync(user);

        return new UserRolesViewModel
        {
            UserId = userId,
            UserName = user.UserName,
            Roles = allRoles.Select(role => new CheckBoxViewModel
            {
                RoleId = role.Id,
                DisplayValue = role.Name,
                IsSelected = userRoles.Contains(role.Name)
            }).ToList()
        };
    }



    public async Task<IdentityResult> UpdateUserRolesAsync(UserRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null) return IdentityResult.Failed();

        var userRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, userRoles);
        return await _userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(r => r.DisplayValue));
    }

    public async Task<Message> GetLastMessageAsync(string userId, string recipientId)
    {
        return await _dbContext.Messages
            .Where(m => (m.UserID == userId && m.RecipientID == recipientId) ||
                        (m.UserID == recipientId && m.RecipientID == userId))
            .OrderByDescending(m => m.When)
            .FirstOrDefaultAsync();

    }

}
