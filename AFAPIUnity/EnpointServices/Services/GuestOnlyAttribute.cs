using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class GuestOnlyAttribute : AuthorizeAttribute, IAuthorizationRequirement
{
    public GuestOnlyAttribute() { }
}

public class GuestOnlyHandler : AuthorizationHandler<GuestOnlyAttribute>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GuestOnlyAttribute requirement)
    {
        if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "**************************************"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
