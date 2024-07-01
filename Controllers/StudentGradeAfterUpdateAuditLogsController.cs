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
    public class StudentGradeAfterUpdateAuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentGradeAfterUpdateAuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentGradeAfterUpdateAuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentGradeAfterUpdateAuditLog.ToListAsync());
        }

        // GET: StudentGradeAfterUpdateAuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGradeAfterUpdateAuditLog = await _context.StudentGradeAfterUpdateAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentGradeAfterUpdateAuditLog == null)
            {
                return NotFound();
            }

            return View(studentGradeAfterUpdateAuditLog);
        }

        // GET: StudentGradeAfterUpdateAuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentGradeAfterUpdateAuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentGradeId,CourseId,StudentId,Grade,DateTaken,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,UserId,Action")] StudentGradeAfterUpdateAuditLog studentGradeAfterUpdateAuditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentGradeAfterUpdateAuditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentGradeAfterUpdateAuditLog);
        }

        // GET: StudentGradeAfterUpdateAuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGradeAfterUpdateAuditLog = await _context.StudentGradeAfterUpdateAuditLog.FindAsync(id);
            if (studentGradeAfterUpdateAuditLog == null)
            {
                return NotFound();
            }
            return View(studentGradeAfterUpdateAuditLog);
        }

        // POST: StudentGradeAfterUpdateAuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentGradeId,CourseId,StudentId,Grade,DateTaken,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,UserId,Action")] StudentGradeAfterUpdateAuditLog studentGradeAfterUpdateAuditLog)
        {
            if (id != studentGradeAfterUpdateAuditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentGradeAfterUpdateAuditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentGradeAfterUpdateAuditLogExists(studentGradeAfterUpdateAuditLog.Id))
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
            return View(studentGradeAfterUpdateAuditLog);
        }

        // GET: StudentGradeAfterUpdateAuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGradeAfterUpdateAuditLog = await _context.StudentGradeAfterUpdateAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentGradeAfterUpdateAuditLog == null)
            {
                return NotFound();
            }

            return View(studentGradeAfterUpdateAuditLog);
        }

        // POST: StudentGradeAfterUpdateAuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentGradeAfterUpdateAuditLog = await _context.StudentGradeAfterUpdateAuditLog.FindAsync(id);
            _context.StudentGradeAfterUpdateAuditLog.Remove(studentGradeAfterUpdateAuditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentGradeAfterUpdateAuditLogExists(int id)
        {
            return _context.StudentGradeAfterUpdateAuditLog.Any(e => e.Id == id);
        }
    }
}
