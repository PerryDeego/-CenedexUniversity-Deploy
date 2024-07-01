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
    public class MainMenuAfterDeleteAuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainMenuAfterDeleteAuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MainMenuAfterDeleteAuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.MainMenuAfterDeleteAuditLog.ToListAsync());
        }

        // GET: MainMenuAfterDeleteAuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainMenuAfterDeleteAuditLog = await _context.MainMenuAfterDeleteAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainMenuAfterDeleteAuditLog == null)
            {
                return NotFound();
            }

            return View(mainMenuAfterDeleteAuditLog);
        }

        // GET: MainMenuAfterDeleteAuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainMenuAfterDeleteAuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MainMenuId,MenuName,MenuUrl,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt,UserId,Action")] MainMenuAfterDeleteAuditLog mainMenuAfterDeleteAuditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mainMenuAfterDeleteAuditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mainMenuAfterDeleteAuditLog);
        }

        // GET: MainMenuAfterDeleteAuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainMenuAfterDeleteAuditLog = await _context.MainMenuAfterDeleteAuditLog.FindAsync(id);
            if (mainMenuAfterDeleteAuditLog == null)
            {
                return NotFound();
            }
            return View(mainMenuAfterDeleteAuditLog);
        }

        // POST: MainMenuAfterDeleteAuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MainMenuId,MenuName,MenuUrl,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt,UserId,Action")] MainMenuAfterDeleteAuditLog mainMenuAfterDeleteAuditLog)
        {
            if (id != mainMenuAfterDeleteAuditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mainMenuAfterDeleteAuditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainMenuAfterDeleteAuditLogExists(mainMenuAfterDeleteAuditLog.Id))
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
            return View(mainMenuAfterDeleteAuditLog);
        }

        // GET: MainMenuAfterDeleteAuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainMenuAfterDeleteAuditLog = await _context.MainMenuAfterDeleteAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainMenuAfterDeleteAuditLog == null)
            {
                return NotFound();
            }

            return View(mainMenuAfterDeleteAuditLog);
        }

        // POST: MainMenuAfterDeleteAuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mainMenuAfterDeleteAuditLog = await _context.MainMenuAfterDeleteAuditLog.FindAsync(id);
            _context.MainMenuAfterDeleteAuditLog.Remove(mainMenuAfterDeleteAuditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MainMenuAfterDeleteAuditLogExists(int id)
        {
            return _context.MainMenuAfterDeleteAuditLog.Any(e => e.Id == id);
        }
    }
}
