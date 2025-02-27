﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CenedexUniversityWebSystem.Permission
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler() { }

        // Use to authorization handler that verifies if a user has the needed permission to access the resource.
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }
            var permissionss = context.User.Claims.Where(x => x.Type == "Permission" && x.Value == requirement.Permission && x.Issuer == "LOCAL AUTHORITY");

            if (permissionss.Any())
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
