using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Contants;
using ServiceMaintenance.ViewModel;
using System.Security.Claims;

public class RolesService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<List<IdentityRole>> GetRolesAsync()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    public async Task<IdentityResult> CreateRoleAsync(string roleName)
    {
        return await _roleManager.CreateAsync(new IdentityRole(roleName));
    }

    public async Task DeleteRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
        }
    }

    public async Task<IdentityRole> GetRoleByIdAsync(string roleId)
    {
        return await _roleManager.FindByIdAsync(roleId);
    }

    public async Task<IList<Claim>> GetRoleClaimsAsync(IdentityRole role)
    {
        return await _roleManager.GetClaimsAsync(role);
    }

    public async Task<IdentityResult> AddClaimAsync(IdentityRole role, Claim claim)
    {
        return await _roleManager.AddClaimAsync(role, claim);
    }

    public async Task<IdentityResult> RemoveClaimAsync(IdentityRole role, Claim claim)
    {
        return await _roleManager.RemoveClaimAsync(role, claim);
    }

    public async Task<PermissionsFormViewModel> GetPermissionsAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return null;
        }

        var roleClaims = await _roleManager.GetClaimsAsync(role);
        var allPermissions = Permissions.GenerateAllPermissions();
        var roleClaimsList = roleClaims.Select(c => c.Value).ToList();

        var permissions = allPermissions.Select(p => new CheckBoxViewModel
        {
            DisplayValue = p,
            IsSelected = roleClaimsList.Contains(p)
        }).ToList();

        return new PermissionsFormViewModel
        {
            RoleId = roleId,
            RoleName = role.Name,
            RoleCalims = permissions
        };
    }
    public async Task<IdentityResult> UpdateRoleAsync(string roleId, string newRoleName)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Role not found." });
        }

        role.Name = newRoleName;
        return await _roleManager.UpdateAsync(role);
    }
    public async Task UpdatePermissionsAsync(PermissionsFormViewModel model)
    {
        if (model == null || string.IsNullOrEmpty(model.RoleId))
        {
            throw new ArgumentException("Invalid role or permissions model.");
        }

        var role = await _roleManager.FindByIdAsync(model.RoleId);
        if (role == null)
        {
            throw new InvalidOperationException("Role not found.");
        }

        // Remove existing claims
        var existingClaims = await _roleManager.GetClaimsAsync(role);
        foreach (var claim in existingClaims)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }

        // Add new claims
        foreach (var permission in model.RoleCalims.Where(c => c.IsSelected))
        {
            var claim = new Claim("Permission", permission.DisplayValue);
            await _roleManager.AddClaimAsync(role, claim);
        }
    }

}
