using EmployeeManagementMVC.Data;
using EmployeeManagementMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementMVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var admins = await _context.Admins.ToListAsync();
            return View(admins);
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.AdminId == id);

            if (admin == null)
                return NotFound();

            return View(admin);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Email")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Admin created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
                return NotFound();

            return View(admin);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,Username,Password,Email")] Admin admin)
        {
            if (id != admin.AdminId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Admin updated successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AdminExists(admin.AdminId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.AdminId == id);

            if (admin == null)
                return NotFound();

            return View(admin);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin != null)
            {
                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Admin deleted successfully.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AdminExists(int id)
        {
            return await _context.Admins.AnyAsync(a => a.AdminId == id);
        }
    }
}
