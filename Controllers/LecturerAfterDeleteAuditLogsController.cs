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
    public class LecturerAfterDeleteAuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturerAfterDeleteAuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LecturerAfterDeleteAuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.LecturerAfterDeleteAuditLog.ToListAsync());
        }

        // GET: LecturerAfterDeleteAuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerAfterDeleteAuditLog = await _context.LecturerAfterDeleteAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturerAfterDeleteAuditLog == null)
            {
                return NotFound();
            }

            return View(lecturerAfterDeleteAuditLog);
        }

        // GET: LecturerAfterDeleteAuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LecturerAfterDeleteAuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LecturerId,Name,DateHired,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt,UserId,Action")] LecturerAfterDeleteAuditLog lecturerAfterDeleteAuditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecturerAfterDeleteAuditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lecturerAfterDeleteAuditLog);
        }

        // GET: LecturerAfterDeleteAuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerAfterDeleteAuditLog = await _context.LecturerAfterDeleteAuditLog.FindAsync(id);
            if (lecturerAfterDeleteAuditLog == null)
            {
                return NotFound();
            }
            return View(lecturerAfterDeleteAuditLog);
        }

        // POST: LecturerAfterDeleteAuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LecturerId,Name,DateHired,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt,UserId,Action")] LecturerAfterDeleteAuditLog lecturerAfterDeleteAuditLog)
        {
            if (id != lecturerAfterDeleteAuditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecturerAfterDeleteAuditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecturerAfterDeleteAuditLogExists(lecturerAfterDeleteAuditLog.Id))
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
            return View(lecturerAfterDeleteAuditLog);
        }

        // GET: LecturerAfterDeleteAuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerAfterDeleteAuditLog = await _context.LecturerAfterDeleteAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturerAfterDeleteAuditLog == null)
            {
                return NotFound();
            }

            return View(lecturerAfterDeleteAuditLog);
        }

        // POST: LecturerAfterDeleteAuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecturerAfterDeleteAuditLog = await _context.LecturerAfterDeleteAuditLog.FindAsync(id);
            _context.LecturerAfterDeleteAuditLog.Remove(lecturerAfterDeleteAuditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LecturerAfterDeleteAuditLogExists(int id)
        {
            return _context.LecturerAfterDeleteAuditLog.Any(e => e.Id == id);
        }
    }
}
