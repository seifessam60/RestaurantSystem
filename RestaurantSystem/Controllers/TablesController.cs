using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using System.Threading.Tasks;

namespace RestaurantSystem.Controllers
{
    public class TablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tables
        public async Task<IActionResult> Index()
        {
            var tables = await _context.Tables
                .OrderBy(t => t.TableNumber)
                .ToListAsync();
            return View(tables);
        }
        // GET: Tables/Details/Id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var table = await _context.Tables
                .Include(t =>t.Orders)
                .ThenInclude(o => o.OrderItems)
                .OrderBy(t=>t.TableNumber)
                .FirstOrDefaultAsync(t=>t.Id == id);
            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }

        // GET: Tables/Create
        public  IActionResult Create()
        {
            return View();
        }

        // POST: Tables/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Table table)
        {
            if (ModelState.IsValid)
            {
                _context.Tables.Add(table);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"تم إضافة الطاولة رقم {table.TableNumber} بنجاح!";
                return RedirectToAction(nameof(Index));
            }
            return View(table);
        }

        // GET: Tables/Edit/Id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var table = await _context.Tables.FindAsync(id);
            if(table == null)
            {
                return NotFound();
            }
            return View(table);
        }

        // POST: Tables/Edit/Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Table table)
        {
            if (id != table.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(table);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"تم تحديث الطاولة رقم {table.TableNumber} بنجاح!";

                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!TableExists(table.Id))
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
            return View(table);
        }
        // POST: Tables/ToggleAvailability/Id
        [HttpPost]
        public async Task<IActionResult> ToggleAvailability(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table != null)
            {
                table.IsAvailable = !table.IsAvailable;
                await _context.SaveChangesAsync();

                string status = table.IsAvailable ? "متاحة" : "محجوزة";
                TempData["Success"] = $"الطاولة رقم {table.TableNumber} الآن {status}";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TableExists(int id)
        {
            return _context.Tables.Any(e => e.Id == id);
        }
    }
}
