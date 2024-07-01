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
    public class CourseAfterUpdateAuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseAfterUpdateAuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseAfterUpdateAuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseAfterUpdateAuditLog.ToListAsync());
        }

        // GET: CourseAfterUpdateAuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseAfterUpdateAuditLog = await _context.CourseAfterUpdateAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseAfterUpdateAuditLog == null)
            {
                return NotFound();
            }

            return View(courseAfterUpdateAuditLog);
        }

        // GET: CourseAfterUpdateAuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseAfterUpdateAuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,Name,Credit,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,UserId")] CourseAfterUpdateAuditLog courseAfterUpdateAuditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseAfterUpdateAuditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseAfterUpdateAuditLog);
        }

        // GET: CourseAfterUpdateAuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseAfterUpdateAuditLog = await _context.CourseAfterUpdateAuditLog.FindAsync(id);
            if (courseAfterUpdateAuditLog == null)
            {
                return NotFound();
            }
            return View(courseAfterUpdateAuditLog);
        }

        // POST: CourseAfterUpdateAuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,Name,Credit,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,UserId")] CourseAfterUpdateAuditLog courseAfterUpdateAuditLog)
        {
            if (id != courseAfterUpdateAuditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseAfterUpdateAuditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseAfterUpdateAuditLogExists(courseAfterUpdateAuditLog.Id))
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
            return View(courseAfterUpdateAuditLog);
        }

        // GET: CourseAfterUpdateAuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseAfterUpdateAuditLog = await _context.CourseAfterUpdateAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseAfterUpdateAuditLog == null)
            {
                return NotFound();
            }

            return View(courseAfterUpdateAuditLog);
        }

        // POST: CourseAfterUpdateAuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseAfterUpdateAuditLog = await _context.CourseAfterUpdateAuditLog.FindAsync(id);
            _context.CourseAfterUpdateAuditLog.Remove(courseAfterUpdateAuditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseAfterUpdateAuditLogExists(int id)
        {
            return _context.CourseAfterUpdateAuditLog.Any(e => e.Id == id);
        }
    }
}
