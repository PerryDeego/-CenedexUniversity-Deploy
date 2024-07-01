/******Created by D.Perry | dd.perry @hotmail.com******/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CenedexUniversityWebSystem.Data;
using CenedexUniversityWebSystem.Models;
using System.Security.Claims;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace CenedexUniversityWebSystem.Controllers
{
    public class StudentGradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public StudentGradesController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /*
        // GET: StudentGrades
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StudentGrade.Include(s => s.Course).Include(s => s.Student);
            return View(await applicationDbContext.ToListAsync());
        }
        */

        // GET: StudentGrades
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            int pageSize = 10; // Get 10 students for each requested page.
            var studentGrades = await _context.StudentGrade.Include(s => s.Course).Include(s => s.Student).ToListAsync();

            var pagedList = studentGrades.ToPagedList(pageNumber, pageSize); // modify
            return View("Index", pagedList); // modify
        }

        // GET: StudentGrades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.StudentGrade
                .Include(s => s.Course)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentGrade == null)
            {
                return NotFound();
            }

            return View(studentGrade);
        }

        // GET: StudentGrades/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName");
            return View();
        }

        // POST: StudentGrades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,StudentId,Grade,DateTaken")] StudentGrade studentGrade)
        {

            studentGrade.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            studentGrade.CreatedBy = this.User.FindFirstValue(ClaimTypes.Name); // will give the user's userId
            studentGrade.CreatedAt = DateTime.Now;

            if (ModelState.IsValid && studentGrade.UserId != null)
            {
                _context.Add(studentGrade);
                await _context.SaveChangesAsync();

                // Success message.
                TempData["mssg"] = "Record saved successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", studentGrade.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", studentGrade.StudentId);
            return View(studentGrade);
        }

        // GET: StudentGrades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.StudentGrade.FindAsync(id);
            if (studentGrade == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", studentGrade.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", studentGrade.StudentId);
            return View(studentGrade);
        }

        // POST: StudentGrades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,StudentId,Grade,DateTaken")] StudentGrade studentGrade)
        {
            studentGrade.ModifiedBy = this.User.FindFirstValue(ClaimTypes.Name); // Will give the user's userName.
            studentGrade.ModifiedAt = DateTime.Now;

            if (id != studentGrade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentGrade);

                    // Success message.
                    TempData["mssg"] = "Record updated successfully!";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentGradeExists(studentGrade.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", studentGrade.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", studentGrade.StudentId);
            return View(studentGrade);
        }

        // GET: StudentGrades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.StudentGrade
                .Include(s => s.Course)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentGrade == null)
            {
                return NotFound();
            }

            return View(studentGrade);
        }

        // POST: StudentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentGrade = await _context.StudentGrade.FindAsync(id);
            _context.StudentGrade.Remove(studentGrade);
            await _context.SaveChangesAsync();

            // Success message.
            TempData["mssg"] = "Record deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CreateReport(string FirstName, string LastName, string CousreName, DateTime? FromDate, DateTime? ToDate)
        {
            // Set parameters for procedure.
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@fName", FirstName),
                new SqlParameter("@lName", LastName),
                new SqlParameter("@cName", CousreName),
                new SqlParameter("@fromDate", FromDate),
                new SqlParameter("@toDate", ToDate),
            };

            // var studentGradeReport = _context.StudentGrade.FromSqlRaw("EXEC [Identity].[sp_course_taken_by_date] {0}, {1}, {2}, {3}, {4}", FirstName, LastName, CousreName, FromDate, ToDate).ToList();
            var studentGradeReport = _context.CourseReportViewModel.FromSqlRaw("[Identity].[sp_course_taken_by_date] {0}, {1}, {2}, {3}, {4}", param).ToList();

            if (studentGradeReport == null)
            {
                return NotFound();
            }

            return View(studentGradeReport);
        }


        private bool StudentGradeExists(int id)
        {
            return _context.StudentGrade.Any(e => e.Id == id);
        }

        

        
        
    }
}
