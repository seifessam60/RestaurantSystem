using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using System.Threading.Tasks;

namespace RestaurantSystem.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuItemsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: MenuItems
        public async Task<IActionResult> Index()
        {
            var menuItems = await _context.MenuItems
                .Include(m => m.Category)
                .OrderBy(m => m.CategoryId)
                .ThenBy(m => m.Name)
                .ToListAsync();
            return View(menuItems);
        }
        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var menuItem = await _context.MenuItems
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if(menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }
    }
}
