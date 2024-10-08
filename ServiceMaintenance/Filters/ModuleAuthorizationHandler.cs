using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

public class ModuleAuthorizationHandler : AuthorizationHandler<ModuleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ModuleRequirement requirement)
    {
        var userClaims = context.User.Claims;

        // Check if the user has the module access permission
        var hasAccess = userClaims.Any(claim =>
            claim.Type == "Permission" &&
            claim.Value == $"Permissions.{requirement.ModuleName}.Access");

        if (hasAccess)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public class ModuleRequirement : IAuthorizationRequirement
{
    public string ModuleName { get; }

    public ModuleRequirement(string moduleName)
    {
        ModuleName = moduleName;
    }
}
