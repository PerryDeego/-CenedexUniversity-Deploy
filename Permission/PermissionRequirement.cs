using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CenedexUniversityWebSystem.Permission
{
    internal class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        // Use to help evaluate permission.
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
