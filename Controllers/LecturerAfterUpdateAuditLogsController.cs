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
    public class LecturerAfterUpdateAuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturerAfterUpdateAuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LecturerAfterUpdateAuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.LecturerAfterUpdateAuditLog.ToListAsync());
        }

        // GET: LecturerAfterUpdateAuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerAfterUpdateAuditLog = await _context.LecturerAfterUpdateAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturerAfterUpdateAuditLog == null)
            {
                return NotFound();
            }

            return View(lecturerAfterUpdateAuditLog);
        }

        // GET: LecturerAfterUpdateAuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LecturerAfterUpdateAuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LecturerId,Name,DateHired,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,UserId,Action")] LecturerAfterUpdateAuditLog lecturerAfterUpdateAuditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecturerAfterUpdateAuditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lecturerAfterUpdateAuditLog);
        }

        // GET: LecturerAfterUpdateAuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerAfterUpdateAuditLog = await _context.LecturerAfterUpdateAuditLog.FindAsync(id);
            if (lecturerAfterUpdateAuditLog == null)
            {
                return NotFound();
            }
            return View(lecturerAfterUpdateAuditLog);
        }

        // POST: LecturerAfterUpdateAuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LecturerId,Name,DateHired,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,UserId,Action")] LecturerAfterUpdateAuditLog lecturerAfterUpdateAuditLog)
        {
            if (id != lecturerAfterUpdateAuditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecturerAfterUpdateAuditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecturerAfterUpdateAuditLogExists(lecturerAfterUpdateAuditLog.Id))
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
            return View(lecturerAfterUpdateAuditLog);
        }

        // GET: LecturerAfterUpdateAuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerAfterUpdateAuditLog = await _context.LecturerAfterUpdateAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturerAfterUpdateAuditLog == null)
            {
                return NotFound();
            }

            return View(lecturerAfterUpdateAuditLog);
        }

        // POST: LecturerAfterUpdateAuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecturerAfterUpdateAuditLog = await _context.LecturerAfterUpdateAuditLog.FindAsync(id);
            _context.LecturerAfterUpdateAuditLog.Remove(lecturerAfterUpdateAuditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LecturerAfterUpdateAuditLogExists(int id)
        {
            return _context.LecturerAfterUpdateAuditLog.Any(e => e.Id == id);
        }
    }
}
