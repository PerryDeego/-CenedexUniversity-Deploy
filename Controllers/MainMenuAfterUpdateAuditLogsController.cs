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
    public class MainMenuAfterUpdateAuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainMenuAfterUpdateAuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MainMenuAfterUpdateAuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.MainMenuAfterUpdateAuditLog.ToListAsync());
        }

        // GET: MainMenuAfterUpdateAuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainMenuAfterUpdateAuditLog = await _context.MainMenuAfterUpdateAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainMenuAfterUpdateAuditLog == null)
            {
                return NotFound();
            }

            return View(mainMenuAfterUpdateAuditLog);
        }

        // GET: MainMenuAfterUpdateAuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainMenuAfterUpdateAuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MainMenuId,MenuName,MenuUrl,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,UserId,Action")] MainMenuAfterUpdateAuditLog mainMenuAfterUpdateAuditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mainMenuAfterUpdateAuditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mainMenuAfterUpdateAuditLog);
        }

        // GET: MainMenuAfterUpdateAuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainMenuAfterUpdateAuditLog = await _context.MainMenuAfterUpdateAuditLog.FindAsync(id);
            if (mainMenuAfterUpdateAuditLog == null)
            {
                return NotFound();
            }
            return View(mainMenuAfterUpdateAuditLog);
        }

        // POST: MainMenuAfterUpdateAuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MainMenuId,MenuName,MenuUrl,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,UserId,Action")] MainMenuAfterUpdateAuditLog mainMenuAfterUpdateAuditLog)
        {
            if (id != mainMenuAfterUpdateAuditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mainMenuAfterUpdateAuditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainMenuAfterUpdateAuditLogExists(mainMenuAfterUpdateAuditLog.Id))
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
            return View(mainMenuAfterUpdateAuditLog);
        }

        // GET: MainMenuAfterUpdateAuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainMenuAfterUpdateAuditLog = await _context.MainMenuAfterUpdateAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainMenuAfterUpdateAuditLog == null)
            {
                return NotFound();
            }

            return View(mainMenuAfterUpdateAuditLog);
        }

        // POST: MainMenuAfterUpdateAuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mainMenuAfterUpdateAuditLog = await _context.MainMenuAfterUpdateAuditLog.FindAsync(id);
            _context.MainMenuAfterUpdateAuditLog.Remove(mainMenuAfterUpdateAuditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MainMenuAfterUpdateAuditLogExists(int id)
        {
            return _context.MainMenuAfterUpdateAuditLog.Any(e => e.Id == id);
        }
    }
}
