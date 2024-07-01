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
    public class StudentAfterUpdateAuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentAfterUpdateAuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentAfterUpdateAuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentAfterUpdateAuditLog.ToListAsync());
        }

        // GET: StudentAfterUpdateAuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAfterUpdateAuditLog = await _context.StudentAfterUpdateAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentAfterUpdateAuditLog == null)
            {
                return NotFound();
            }

            return View(studentAfterUpdateAuditLog);
        }

        // GET: StudentAfterUpdateAuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentAfterUpdateAuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,FirstName,LastName,DateOfBirth,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,UserId,Action")] StudentAfterUpdateAuditLog studentAfterUpdateAuditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentAfterUpdateAuditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentAfterUpdateAuditLog);
        }

        // GET: StudentAfterUpdateAuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAfterUpdateAuditLog = await _context.StudentAfterUpdateAuditLog.FindAsync(id);
            if (studentAfterUpdateAuditLog == null)
            {
                return NotFound();
            }
            return View(studentAfterUpdateAuditLog);
        }

        // POST: StudentAfterUpdateAuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,FirstName,LastName,DateOfBirth,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,UserId,Action")] StudentAfterUpdateAuditLog studentAfterUpdateAuditLog)
        {
            if (id != studentAfterUpdateAuditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentAfterUpdateAuditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentAfterUpdateAuditLogExists(studentAfterUpdateAuditLog.Id))
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
            return View(studentAfterUpdateAuditLog);
        }

        // GET: StudentAfterUpdateAuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAfterUpdateAuditLog = await _context.StudentAfterUpdateAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentAfterUpdateAuditLog == null)
            {
                return NotFound();
            }

            return View(studentAfterUpdateAuditLog);
        }

        // POST: StudentAfterUpdateAuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentAfterUpdateAuditLog = await _context.StudentAfterUpdateAuditLog.FindAsync(id);
            _context.StudentAfterUpdateAuditLog.Remove(studentAfterUpdateAuditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentAfterUpdateAuditLogExists(int id)
        {
            return _context.StudentAfterUpdateAuditLog.Any(e => e.Id == id);
        }
    }
}
