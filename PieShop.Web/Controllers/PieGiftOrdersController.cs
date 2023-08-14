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
    public class PieGiftOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PieGiftOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PieGiftOrders
        public async Task<IActionResult> Index()
        {
              return _context.PieGiftOrder != null ? 
                          View(await _context.PieGiftOrder.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PieGiftOrder'  is null.");
        }

        // GET: PieGiftOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PieGiftOrder == null)
            {
                return NotFound();
            }

            var pieGiftOrder = await _context.PieGiftOrder
                .FirstOrDefaultAsync(m => m.PieGiftOrderId == id);
            if (pieGiftOrder == null)
            {
                return NotFound();
            }

            return View(pieGiftOrder);
        }

        // GET: PieGiftOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PieGiftOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PieGiftOrderId,Name,Address")] PieGiftOrder pieGiftOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pieGiftOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pieGiftOrder);
        }

        // GET: PieGiftOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PieGiftOrder == null)
            {
                return NotFound();
            }

            var pieGiftOrder = await _context.PieGiftOrder.FindAsync(id);
            if (pieGiftOrder == null)
            {
                return NotFound();
            }
            return View(pieGiftOrder);
        }

        // POST: PieGiftOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PieGiftOrderId,Name,Address")] PieGiftOrder pieGiftOrder)
        {
            if (id != pieGiftOrder.PieGiftOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pieGiftOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PieGiftOrderExists(pieGiftOrder.PieGiftOrderId))
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
            return View(pieGiftOrder);
        }

        // GET: PieGiftOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PieGiftOrder == null)
            {
                return NotFound();
            }

            var pieGiftOrder = await _context.PieGiftOrder
                .FirstOrDefaultAsync(m => m.PieGiftOrderId == id);
            if (pieGiftOrder == null)
            {
                return NotFound();
            }

            return View(pieGiftOrder);
        }

        // POST: PieGiftOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PieGiftOrder == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PieGiftOrder'  is null.");
            }
            var pieGiftOrder = await _context.PieGiftOrder.FindAsync(id);
            if (pieGiftOrder != null)
            {
                _context.PieGiftOrder.Remove(pieGiftOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PieGiftOrderExists(int id)
        {
          return (_context.PieGiftOrder?.Any(e => e.PieGiftOrderId == id)).GetValueOrDefault();
        }
    }
}
