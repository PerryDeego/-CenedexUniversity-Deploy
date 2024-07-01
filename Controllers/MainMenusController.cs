using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CenedexUniversityWebSystem.Data;
using CenedexUniversityWebSystem.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;

namespace CenedexUniversityWebSystem.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MainMenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainMenusController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*
        // GET: MainMenus
        public async Task<IActionResult> Index()
        {
            return View(await _context.MainMenu.ToListAsync());
        }
        */

        // GET: MainMenus
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            int pageSize = 10; // Get 10 students for each requested page.
            var mainMenus = await _context.MainMenu.Include(c => c.User).ToListAsync();

            var pagedList = mainMenus.ToPagedList(pageNumber, pageSize); // modify
            return View("Index", pagedList); // modify
        }


        // GET: MainMenus to generate links.
        public ActionResult GenerateNavBar()
        {

            var menuList = _context.MainMenu.Include(c => c.SubMenus).ToList();
            return View(menuList);
        }


        public ActionResult GenerateMenu()
        {

            var menuList = _context.MainMenu.Include(c => c.SubMenus).ToList();
            return View(menuList);
        }



        // GET: MainMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainMenu = await _context.MainMenu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainMenu == null)
            {
                return NotFound();
            }

            return View(mainMenu);
        }

        // GET: MainMenus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MenuName,MenuUrl")] MainMenu mainMenu)
        {
            mainMenu.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            mainMenu.CreatedBy = this.User.FindFirstValue(ClaimTypes.Name); // will give the user's userId
            mainMenu.CreatedAt = DateTime.Now;

            if (ModelState.IsValid && mainMenu.UserId != null)
            {
                _context.Add(mainMenu);
                await _context.SaveChangesAsync();

                // Success message.
                TempData["mssg"] = "Record saved successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(mainMenu);
        }

        // GET: MainMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainMenu = await _context.MainMenu.FindAsync(id);
            if (mainMenu == null)
            {
                return NotFound();
            }
            return View(mainMenu);
        }

        // POST: MainMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MenuName,MenuUrl")] MainMenu mainMenu)
        {
            mainMenu.ModifiedBy = this.User.FindFirstValue(ClaimTypes.Name); // Will give the user's userName.
            mainMenu.ModifiedAt = DateTime.Now;

            if (id != mainMenu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && mainMenu.ModifiedBy != null)
            {
                try
                {
                    _context.Update(mainMenu);
                    await _context.SaveChangesAsync();
                    // Success message.
                    TempData["mssg"] = "Record updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainMenuExists(mainMenu.Id))
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
            return View(mainMenu);
        }

        // GET: MainMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainMenu = await _context.MainMenu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainMenu == null)
            {
                return NotFound();
            }

            return View(mainMenu);
        }

        // POST: MainMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mainMenu = await _context.MainMenu.FindAsync(id);
            _context.MainMenu.Remove(mainMenu);
            await _context.SaveChangesAsync();

            // Success message.
            TempData["mssg"] = "Record deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool MainMenuExists(int id)
        {
            return _context.MainMenu.Any(e => e.Id == id);
        }
    }
}
