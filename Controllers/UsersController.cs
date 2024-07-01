using CenedexUniversityWebSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace CenedexUniversityWebSystem.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;

        public UsersController(UserManager<IdentityUser> userManager, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();

            return View(allUsersExceptCurrentUser);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {


            if (ModelState.IsValid)
            {

                var user = new IdentityUser { UserName = userViewModel.Email, Email = userViewModel.Email };
                var result = await _userManager.CreateAsync(user, userViewModel.Password);

                if (result.Succeeded)
                {
                    // Success message.
                    TempData["mssg"] = "User account saved successfully!";
                    _logger.LogInformation("User {UserName} account with password created successfully.", userViewModel.Email);
                    _logger.LogInformation("Created user {UserName}.", userViewModel.Email);

                    // Success message.
                    TempData["mssg"] = "User saved successfully!";

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(userViewModel);

        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IdentityUser user = await _userManager.FindByIdAsync(id);

            var useVeiwModel = new UserViewModel { Id = user.Id, Email = user.Email };

            if (useVeiwModel == null)
            {
                return NotFound();
            }
          
            return View(useVeiwModel);

        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel userViewModel)
        {
           IdentityUser user = await _userManager.FindByIdAsync(userViewModel.Id);

            if (userViewModel.Id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Uodate User attributes.
                    user.Email = userViewModel.Email;
                    user.UserName = userViewModel.Email;

                    await _userManager.UpdateAsync(user);
                    // Success message.
                    TempData["mssg"] = "User account saved successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserViewModelExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    _logger.LogInformation("Deleted user {UserName}.", result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", _userManager.Users);
        }


        private bool UserViewModelExists(string email)
        {
            return _userManager.Users.Any(e => e.Email == email);
        }
    }
}
