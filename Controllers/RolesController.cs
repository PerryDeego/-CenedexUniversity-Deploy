using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace CenedexUniversityWebSystem.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;

        public RolesController(RoleManager<IdentityRole> roleManager, ILogger<UsersController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        /*
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        */

        // GET: Roles
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            int pageSize = 10; // Get 10 students for each requested page.
            var roles = await _roleManager.Roles.ToListAsync();

            var pagedList = roles.ToPagedList(pageNumber, pageSize); // modify
            return View("Index", pagedList); // modify
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
                // Success message.
                TempData["mssg"] = "Role saved successfully!";
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    _logger.LogInformation("Deleted user {UserName}.", result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", _roleManager.Roles);
        }

    }
}
