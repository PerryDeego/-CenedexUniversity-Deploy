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
    [Authorize]
    public class LecturersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*
        // GET: Lecturers
        public async Task<IActionResult> Index1()
        {
            var applicationDbContext = _context.Lecturer.Include(l => l.User);
            return View(await applicationDbContext.ToListAsync());
        }
        */


        // GET: Lecturers
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            int pageSize = 10; // Get 10 students for each requested page.
            var lecturers = await _context.Lecturer.Include(c => c.User).ToListAsync();

            var pagedList = lecturers.ToPagedList(pageNumber, pageSize); // modify
            return View("Index", pagedList); // modify
        }


        // GET: Lecturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturer
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // GET: Lecturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lecturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateHired")] Lecturer lecturer)
        {
            lecturer.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            lecturer.CreatedBy = this.User.FindFirstValue(ClaimTypes.Name); // will give the user's userId
            lecturer.CreatedAt = DateTime.Now;

            if (ModelState.IsValid && lecturer.UserId != null)
            {
                _context.Add(lecturer);
                await _context.SaveChangesAsync();
                // Success message.
                TempData["mssg"] = "Record saved successfully!";
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", course.UserId);
            return View(lecturer);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturer.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", lecturer.UserId);
            return View(lecturer);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateHired")] Lecturer lecturer)
        {
            lecturer.ModifiedBy = this.User.FindFirstValue(ClaimTypes.Name); // will give the user's userId
            lecturer.ModifiedAt = DateTime.Now;

            if (id != lecturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && lecturer.ModifiedBy != null)
            {
                try
                {
                    _context.Update(lecturer);
                    await _context.SaveChangesAsync();
                    // Success message.
                    TempData["mssg"] = "Record updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(lecturer.Id))
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

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", lecturer.UserId);
            return View(lecturer);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturer
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecturer = await _context.Lecturer.FindAsync(id);
            _context.Lecturer.Remove(lecturer);
            await _context.SaveChangesAsync();

            // Success message.
            TempData["mssg"] = "Record deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Lecturer.Any(e => e.Id == id);
        }
    }
}
