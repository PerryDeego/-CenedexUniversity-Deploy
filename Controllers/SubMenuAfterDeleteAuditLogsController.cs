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
    public class SubMenuAfterDeleteAuditLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubMenuAfterDeleteAuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubMenuAfterDeleteAuditLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubMenuAfterDeleteAuditLog.ToListAsync());
        }

        // GET: SubMenuAfterDeleteAuditLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenuAfterDeleteAuditLog = await _context.SubMenuAfterDeleteAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subMenuAfterDeleteAuditLog == null)
            {
                return NotFound();
            }

            return View(subMenuAfterDeleteAuditLog);
        }

        // GET: SubMenuAfterDeleteAuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubMenuAfterDeleteAuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubMenuId,SubMenuName,SubMenuUrl,MainMenuId,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt,UserId,Action")] SubMenuAfterDeleteAuditLog subMenuAfterDeleteAuditLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subMenuAfterDeleteAuditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subMenuAfterDeleteAuditLog);
        }

        // GET: SubMenuAfterDeleteAuditLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenuAfterDeleteAuditLog = await _context.SubMenuAfterDeleteAuditLog.FindAsync(id);
            if (subMenuAfterDeleteAuditLog == null)
            {
                return NotFound();
            }
            return View(subMenuAfterDeleteAuditLog);
        }

        // POST: SubMenuAfterDeleteAuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubMenuId,SubMenuName,SubMenuUrl,MainMenuId,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,DeletedBy,DeletedAt,UserId,Action")] SubMenuAfterDeleteAuditLog subMenuAfterDeleteAuditLog)
        {
            if (id != subMenuAfterDeleteAuditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subMenuAfterDeleteAuditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubMenuAfterDeleteAuditLogExists(subMenuAfterDeleteAuditLog.Id))
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
            return View(subMenuAfterDeleteAuditLog);
        }

        // GET: SubMenuAfterDeleteAuditLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenuAfterDeleteAuditLog = await _context.SubMenuAfterDeleteAuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subMenuAfterDeleteAuditLog == null)
            {
                return NotFound();
            }

            return View(subMenuAfterDeleteAuditLog);
        }

        // POST: SubMenuAfterDeleteAuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subMenuAfterDeleteAuditLog = await _context.SubMenuAfterDeleteAuditLog.FindAsync(id);
            _context.SubMenuAfterDeleteAuditLog.Remove(subMenuAfterDeleteAuditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubMenuAfterDeleteAuditLogExists(int id)
        {
            return _context.SubMenuAfterDeleteAuditLog.Any(e => e.Id == id);
        }
    }
}
