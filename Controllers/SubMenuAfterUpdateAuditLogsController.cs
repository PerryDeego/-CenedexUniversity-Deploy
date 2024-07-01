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
    public class SubMenuAfterUpdateAuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubMenuAfterUpdateAuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubMenuAfterUpdateAuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubMenuAfterUpdateAuditLog.ToListAsync());
        }

        // GET: SubMenuAfterUpdateAuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenuAfterUpdateAuditLog = await _context.SubMenuAfterUpdateAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subMenuAfterUpdateAuditLog == null)
            {
                return NotFound();
            }

            return View(subMenuAfterUpdateAuditLog);
        }

        // GET: SubMenuAfterUpdateAuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubMenuAfterUpdateAuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubMenuId,SubMenuName,SubMenuUrl,MainMenuId,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,UserId,Action")] SubMenuAfterUpdateAuditLog subMenuAfterUpdateAuditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subMenuAfterUpdateAuditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subMenuAfterUpdateAuditLog);
        }

        // GET: SubMenuAfterUpdateAuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenuAfterUpdateAuditLog = await _context.SubMenuAfterUpdateAuditLog.FindAsync(id);
            if (subMenuAfterUpdateAuditLog == null)
            {
                return NotFound();
            }
            return View(subMenuAfterUpdateAuditLog);
        }

        // POST: SubMenuAfterUpdateAuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubMenuId,SubMenuName,SubMenuUrl,MainMenuId,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,UserId,Action")] SubMenuAfterUpdateAuditLog subMenuAfterUpdateAuditLog)
        {
            if (id != subMenuAfterUpdateAuditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subMenuAfterUpdateAuditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubMenuAfterUpdateAuditLogExists(subMenuAfterUpdateAuditLog.Id))
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
            return View(subMenuAfterUpdateAuditLog);
        }

        // GET: SubMenuAfterUpdateAuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenuAfterUpdateAuditLog = await _context.SubMenuAfterUpdateAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subMenuAfterUpdateAuditLog == null)
            {
                return NotFound();
            }

            return View(subMenuAfterUpdateAuditLog);
        }

        // POST: SubMenuAfterUpdateAuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subMenuAfterUpdateAuditLog = await _context.SubMenuAfterUpdateAuditLog.FindAsync(id);
            _context.SubMenuAfterUpdateAuditLog.Remove(subMenuAfterUpdateAuditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubMenuAfterUpdateAuditLogExists(int id)
        {
            return _context.SubMenuAfterUpdateAuditLog.Any(e => e.Id == id);
        }
    }
}
