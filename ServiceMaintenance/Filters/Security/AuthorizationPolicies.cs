using Microsoft.AspNetCore.Authorization;
using ServiceMaintenance.Contants;

namespace ServiceMaintenance.Filters.Security
{
    public static class AuthorizationPolicies
    {
        public static void AddPolicies(AuthorizationOptions options)
        {
            options.AddPolicy("CanCreateProducts", policy =>
                policy.RequireClaim("Permission", Permissions.Products.Create));

            options.AddPolicy("ModuleProductsAccess", policy =>
                policy.Requirements.Add(new ModuleRequirement("Products")));

            options.AddPolicy("ModuleDashBoardAccess", policy =>
              policy.Requirements.Add(new ModuleRequirement("DashBoard")));
            options.AddPolicy("ModuleCustomerAccess", policy =>
            policy.Requirements.Add(new ModuleRequirement("Customer")));

        }
    }
}
