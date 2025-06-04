using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


public class UserOnlyHandler : AuthorizationHandler<UserOnlyAttribute>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserOnlyAttribute requirement)
    {
        // Your logic to check if the user is authorized
        // For example, you might want to check user roles, claims, etc.

        // Example check: if user has a specific claim
        if (context.User.HasClaim(c => c.Type == ClaimTypes.Name))
        {
            // If the user meets the requirement
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}


public class UserOnlyAttribute : IAuthorizationRequirement
{
    // You can add any properties here if needed, or keep it empty
}

