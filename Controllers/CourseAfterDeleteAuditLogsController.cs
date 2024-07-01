using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CenedexUniversityWebSystem.Data;
using CenedexUniversityWebSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace CenedexUniversityWebSystem.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class CourseAfterDeleteAuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseAfterDeleteAuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseAfterDeleteAuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseAfterDeleteAuditLog.ToListAsync());
        }

        // GET: CourseAfterDeleteAuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseAfterDeleteAuditLog = await _context.CourseAfterDeleteAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseAfterDeleteAuditLog == null)
            {
                return NotFound();
            }

            return View(courseAfterDeleteAuditLog);
        }

        // GET: CourseAfterDeleteAuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseAfterDeleteAuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,Name,Credit,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt,UserId,Action")] CourseAfterDeleteAuditLog courseAfterDeleteAuditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseAfterDeleteAuditLog);
                await _context.SaveChangesAsync();

                TempData["mssg"] = "Record saved successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(courseAfterDeleteAuditLog);
        }

        // GET: CourseAfterDeleteAuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseAfterDeleteAuditLog = await _context.CourseAfterDeleteAuditLog.FindAsync(id);
            if (courseAfterDeleteAuditLog == null)
            {
                return NotFound();
            }
            return View(courseAfterDeleteAuditLog);
        }

        // POST: CourseAfterDeleteAuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,Name,Credit,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt,UserId,Action")] CourseAfterDeleteAuditLog courseAfterDeleteAuditLog)
        {
            if (id != courseAfterDeleteAuditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseAfterDeleteAuditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseAfterDeleteAuditLogExists(courseAfterDeleteAuditLog.Id))
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
            return View(courseAfterDeleteAuditLog);
        }

        // GET: CourseAfterDeleteAuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseAfterDeleteAuditLog = await _context.CourseAfterDeleteAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseAfterDeleteAuditLog == null)
            {
                return NotFound();
            }

            return View(courseAfterDeleteAuditLog);
        }

        // POST: CourseAfterDeleteAuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseAfterDeleteAuditLog = await _context.CourseAfterDeleteAuditLog.FindAsync(id);
            _context.CourseAfterDeleteAuditLog.Remove(courseAfterDeleteAuditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseAfterDeleteAuditLogExists(int id)
        {
            return _context.CourseAfterDeleteAuditLog.Any(e => e.Id == id);
        }
    }
}
