using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
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
            if (id == null)
            {
                return NotFound();
            }
            var menuItem = await _context.MenuItems
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }

        // GET: MenuItems/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }
        // POST: MenuItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                menuItem.CreatedAt = DateTime.Now;
                _context.MenuItems.Add(menuItem);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"تم إضافة {menuItem.Name} بنجاح!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", menuItem.CategoryId);
            return View(menuItem);
        }

        // GET: MenuItems/Edit/Id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", menuItem.CategoryId);
            return View(menuItem);
        }

        // POST: MenuItems/Edit/Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.MenuItems.Update(menuItem);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"تم تحديث {menuItem.Name} بنجاح!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.Id))
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
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", menuItem.CategoryId);
            return View(menuItem);
        }
        // GET: MenuItems/Delete/Id
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var menuItem = _context.MenuItems
                .Include(m => m.Category)
                .FirstOrDefault(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"تم حذف {menuItem.Name} بنجاح!";
            }

            return RedirectToAction(nameof(Index));
        }

        // Helper method
        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(e => e.Id == id);
        }
    }
}
