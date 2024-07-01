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
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*
       // GET: Courses
       public async Task<IActionResult> Index1()
       {
           var applicationDbContext = _context.Course.Include(c => c.User);
           return View(await applicationDbContext.ToListAsync());
       }
       */

        // GET: Courses
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            int pageSize = 10; // Get 10 students for each requested page.
            var courses = await _context.Course.Include(c => c.User).ToListAsync();

            var pagedList = courses.ToPagedList(pageNumber, pageSize); // modify
            return View("Index", pagedList); // modify
        }


        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
           // ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Credit")] Course course)
        {
            course.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            course.CreatedBy = this.User.FindFirstValue(ClaimTypes.Name); // will give the user's userId
            course.CreatedAt = DateTime.Now;

            if (ModelState.IsValid && course.UserId != null)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                // Success message.
                TempData["mssg"] = "Record saved successfully!";
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", course.UserId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", course.UserId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Credit")] Course course)
        {
            course.ModifiedBy = this.User.FindFirstValue(ClaimTypes.Name); // Will give the user's userName.
            course.ModifiedAt = DateTime.Now;

            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && course.ModifiedBy != null)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    // Success message.
                    TempData["mssg"] = "Record updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
          
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", course.UserId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            // Success message.
            TempData["mssg"] = "Record deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
    }
}
