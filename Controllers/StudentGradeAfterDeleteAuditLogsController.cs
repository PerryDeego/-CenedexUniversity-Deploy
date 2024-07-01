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
    public class StudentGradeAfterDeleteAuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentGradeAfterDeleteAuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentGradeAfterDeleteAuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentGradeAfterDeleteAuditLog.ToListAsync());
        }

        // GET: StudentGradeAfterDeleteAuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGradeAfterDeleteAuditLog = await _context.StudentGradeAfterDeleteAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentGradeAfterDeleteAuditLog == null)
            {
                return NotFound();
            }

            return View(studentGradeAfterDeleteAuditLog);
        }

        // GET: StudentGradeAfterDeleteAuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentGradeAfterDeleteAuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentGradeId,CourseId,StudentId,Grade,DateTaken,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt,UserId,Action")] StudentGradeAfterDeleteAuditLog studentGradeAfterDeleteAuditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentGradeAfterDeleteAuditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentGradeAfterDeleteAuditLog);
        }

        // GET: StudentGradeAfterDeleteAuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGradeAfterDeleteAuditLog = await _context.StudentGradeAfterDeleteAuditLog.FindAsync(id);
            if (studentGradeAfterDeleteAuditLog == null)
            {
                return NotFound();
            }
            return View(studentGradeAfterDeleteAuditLog);
        }

        // POST: StudentGradeAfterDeleteAuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentGradeId,CourseId,StudentId,Grade,DateTaken,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt,UserId,Action")] StudentGradeAfterDeleteAuditLog studentGradeAfterDeleteAuditLog)
        {
            if (id != studentGradeAfterDeleteAuditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentGradeAfterDeleteAuditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentGradeAfterDeleteAuditLogExists(studentGradeAfterDeleteAuditLog.Id))
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
            return View(studentGradeAfterDeleteAuditLog);
        }

        // GET: StudentGradeAfterDeleteAuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGradeAfterDeleteAuditLog = await _context.StudentGradeAfterDeleteAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentGradeAfterDeleteAuditLog == null)
            {
                return NotFound();
            }

            return View(studentGradeAfterDeleteAuditLog);
        }

        // POST: StudentGradeAfterDeleteAuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentGradeAfterDeleteAuditLog = await _context.StudentGradeAfterDeleteAuditLog.FindAsync(id);
            _context.StudentGradeAfterDeleteAuditLog.Remove(studentGradeAfterDeleteAuditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentGradeAfterDeleteAuditLogExists(int id)
        {
            return _context.StudentGradeAfterDeleteAuditLog.Any(e => e.Id == id);
        }
    }
}
