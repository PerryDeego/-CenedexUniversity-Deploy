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
    public class StudentAfterDeleteAuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentAfterDeleteAuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentAfterDeleteAuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentAfterDeleteAuditLog.ToListAsync());
        }

        // GET: StudentAfterDeleteAuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAfterDeleteAuditLog = await _context.StudentAfterDeleteAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentAfterDeleteAuditLog == null)
            {
                return NotFound();
            }

            return View(studentAfterDeleteAuditLog);
        }

        // GET: StudentAfterDeleteAuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentAfterDeleteAuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,FirstName,LastName,DateOfBirth,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt,UserId,Action")] StudentAfterDeleteAuditLog studentAfterDeleteAuditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentAfterDeleteAuditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentAfterDeleteAuditLog);
        }

        // GET: StudentAfterDeleteAuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAfterDeleteAuditLog = await _context.StudentAfterDeleteAuditLog.FindAsync(id);
            if (studentAfterDeleteAuditLog == null)
            {
                return NotFound();
            }
            return View(studentAfterDeleteAuditLog);
        }

        // POST: StudentAfterDeleteAuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,FirstName,LastName,DateOfBirth,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt,UserId,Action")] StudentAfterDeleteAuditLog studentAfterDeleteAuditLog)
        {
            if (id != studentAfterDeleteAuditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentAfterDeleteAuditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentAfterDeleteAuditLogExists(studentAfterDeleteAuditLog.Id))
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
            return View(studentAfterDeleteAuditLog);
        }

        // GET: StudentAfterDeleteAuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAfterDeleteAuditLog = await _context.StudentAfterDeleteAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentAfterDeleteAuditLog == null)
            {
                return NotFound();
            }

            return View(studentAfterDeleteAuditLog);
        }

        // POST: StudentAfterDeleteAuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentAfterDeleteAuditLog = await _context.StudentAfterDeleteAuditLog.FindAsync(id);
            _context.StudentAfterDeleteAuditLog.Remove(studentAfterDeleteAuditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentAfterDeleteAuditLogExists(int id)
        {
            return _context.StudentAfterDeleteAuditLog.Any(e => e.Id == id);
        }
    }
}
