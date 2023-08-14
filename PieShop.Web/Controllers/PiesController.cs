using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PieShop.Core.Models;
using PieShop.Data;

namespace PieShop.Web.Controllers
{
    public class PiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pie.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pie == null)
            {
                return NotFound();
            }

            var pie = await _context.Pie
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PieId == id);
            if (pie == null)
            {
                return NotFound();
            }

            return View(pie);
        }

        // GET: Pies/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Pies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PieId,Name,ShortDescription,LongDescription,AllergyInformation,Price,ImageUrl,ImageThumbnailUrl,IsPieOfTheWeek,InStock,CategoryId,SugarLevel")] Pie pie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryName", pie.CategoryId);
            return View(pie);
        }

        // GET: Pies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pie == null)
            {
                return NotFound();
            }

            var pie = await _context.Pie.FindAsync(id);
            if (pie == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryName", pie.CategoryId);
            return View(pie);
        }

        // POST: Pies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PieId,Name,ShortDescription,LongDescription,AllergyInformation,Price,ImageUrl,ImageThumbnailUrl,IsPieOfTheWeek,InStock,CategoryId,SugarLevel")] Pie pie)
        {
            if (id != pie.PieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PieExists(pie.PieId))
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
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryName", pie.CategoryId);
            return View(pie);
        }

        // GET: Pies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pie == null)
            {
                return NotFound();
            }

            var pie = await _context.Pie
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PieId == id);
            if (pie == null)
            {
                return NotFound();
            }

            return View(pie);
        }

        // POST: Pies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pie == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pie'  is null.");
            }
            var pie = await _context.Pie.FindAsync(id);
            if (pie != null)
            {
                _context.Pie.Remove(pie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PieExists(int id)
        {
          return (_context.Pie?.Any(e => e.PieId == id)).GetValueOrDefault();
        }
    }
}
