using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CenedexUniversityWebSystem.Models;
using CenedexUniversityWebSystem.Data;

namespace CenedexUniversityWebSystem.ViewComponents
{
    public class MainMenu: ViewComponent
    {

        private readonly ApplicationDbContext _context;
        public MainMenu(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menuList =  await _context.MainMenu.Include(c => c.SubMenus).ToListAsync();
            return View(menuList);
        }

    }
}
