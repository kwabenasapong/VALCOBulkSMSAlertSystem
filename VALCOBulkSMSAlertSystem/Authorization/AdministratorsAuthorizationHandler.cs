using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using VALCOBulkSMSAlertSystem.Models;
using static VALCOBulkSMSAlertSystem.Authorization.Operations;

namespace VALCOBulkSMSAlertSystem.Authorization
{
    public class AdministratorsAuthorizationHandler
                    : AuthorizationHandler<OperationAuthorizationRequirement, Messages>
    {
        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                     Messages resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.AdministratorsRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
