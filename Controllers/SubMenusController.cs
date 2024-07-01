using System;
using System.Web;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CenedexUniversityWebSystem.Data;
using CenedexUniversityWebSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;

namespace CenedexUniversityWebSystem.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SubMenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubMenusController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*
        // GET: SubMenus
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubMenu.Include(s => s.MainMenu);
            return View(await applicationDbContext.ToListAsync());
        }
        */

        // GET: SubMenus
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            int pageSize = 10; // Get 10 students for each requested page.
            var subMenus = await _context.SubMenu.Include(c => c.User).ToListAsync();

            var pagedList = subMenus.ToPagedList(pageNumber, pageSize); // modify
            return View("Index", pagedList); // modify
        }

        // GET: SubMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenu = await _context.SubMenu
                .Include(s => s.MainMenu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subMenu == null)
            {
                return NotFound();
            }

            return View(subMenu);
        }

        // GET: SubMenus/Create
        public IActionResult Create()
        {
            ViewData["MainMenuId"] = new SelectList(_context.Set<MainMenu>(), "Id", "MenuName");
            return View();
        }

        // POST: SubMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubMenuName,SubMenuUrl,MainMenuId")] SubMenu subMenu)
        {

            subMenu.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            subMenu.CreatedBy = this.User.FindFirstValue(ClaimTypes.Name); // will give the user's userId
            subMenu.CreatedAt = DateTime.Now;

            if (ModelState.IsValid && subMenu.UserId != null)
            {
                _context.Add(subMenu);
                await _context.SaveChangesAsync();

                // Success message.
                TempData["mssg"] = "Record saved successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainMenuId"] = new SelectList(_context.Set<MainMenu>(), "Id", "MenuName", subMenu.MainMenuId);
            return View(subMenu);
        }

        // GET: SubMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var subMenu = await _context.SubMenu.FindAsync(id);
            if (subMenu == null)
            {
                return NotFound();
            }
            ViewData["MainMenuId"] = new SelectList(_context.Set<MainMenu>(), "Id", "MenuName", subMenu.MainMenuId);
            return View(subMenu);
        }

        // POST: SubMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubMenuName,SubMenuUrl,MainMenuId")] SubMenu subMenu)
        {

            subMenu.ModifiedBy = this.User.FindFirstValue(ClaimTypes.Name); // Will give the user's userName.
            subMenu.ModifiedAt = DateTime.Now;

            if (id != subMenu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && subMenu.ModifiedBy != null)
            {
                try
                {
                    _context.Update(subMenu);

                    // Success message.
                    TempData["mssg"] = "Record updated successfully!";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubMenuExists(subMenu.Id))
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
            ViewData["MainMenuId"] = new SelectList(_context.Set<MainMenu>(), "Id", "MenuName", subMenu.MainMenuId);
            return View(subMenu);
        }

        // GET: SubMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenu = await _context.SubMenu
                .Include(s => s.MainMenu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subMenu == null)
            {
                return NotFound();
            }

            return View(subMenu);
        }

        // POST: SubMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subMenu = await _context.SubMenu.FindAsync(id);
            _context.SubMenu.Remove(subMenu);
            await _context.SaveChangesAsync();

            // Success message.
            TempData["mssg"] = "Record deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool SubMenuExists(int id)
        {
            return _context.SubMenu.Any(e => e.Id == id);
        }
    }
}
