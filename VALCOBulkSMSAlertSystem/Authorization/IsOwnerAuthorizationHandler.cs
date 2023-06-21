﻿using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using VALCOBulkSMSAlertSystem.Models;
using static VALCOBulkSMSAlertSystem.Authorization.Operations;
using VALCOBulkSMSAlertSystem.Areas.Identity.Data;

namespace VALCOBulkSMSAlertSystem.Authorization
{
    public class IsOwnerAuthorizationHandler
                : AuthorizationHandler<OperationAuthorizationRequirement, Messages>
    {
        UserManager<VALCOUser> _userManager;

        public IsOwnerAuthorizationHandler(UserManager<VALCOUser>
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Messages resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for CRUD permission, return.

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.UserID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
