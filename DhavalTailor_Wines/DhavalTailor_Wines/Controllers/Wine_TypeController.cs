using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DhavalTailor_Wines.Data;
using DhavalTailor_Wines.Models;

namespace DhavalTailor_Wines.Controllers
{
    public class Wine_TypeController : Controller
    {
        private readonly WinesContext _context;

        public Wine_TypeController(WinesContext context)
        {
            _context = context;
        }

        // GET: Wine_Type
        public async Task<IActionResult> Index()
        {
              return View(await _context.Wine_Types.ToListAsync());
        }

        // GET: Wine_Type/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Wine_Types == null)
            {
                return NotFound();
            }

            var wine_Type = await _context.Wine_Types
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wine_Type == null)
            {
                return NotFound();
            }

            return View(wine_Type);
        }

        // GET: Wine_Type/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wine_Type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,WineTypeName")] Wine_Type wine_Type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(wine_Type);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {

                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
                {
                    ModelState.AddModelError("Wine Type", "Unable to save changes. Remember, you cannot have duplicate Wine Types.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(wine_Type);
        }

        // GET: Wine_Type/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Wine_Types == null)
            {
                return NotFound();
            }

            var wine_Type = await _context.Wine_Types.FindAsync(id);
            if (wine_Type == null)
            {
                return NotFound();
            }
            return View(wine_Type);
        }

        // POST: Wine_Type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,WineTypeName")] Wine_Type wine_Type)
        {
            if (id != wine_Type.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wine_Type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Wine_TypeExists(wine_Type.ID))
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
            return View(wine_Type);
        }

        // GET: Wine_Type/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Wine_Types == null)
            {
                return NotFound();
            }

            var wine_Type = await _context.Wine_Types
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wine_Type == null)
            {
                return NotFound();
            }

            return View(wine_Type);
        }

        // POST: Wine_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Wine_Types == null)
            {
                return Problem("Entity set 'WinesContext.Wine_Types'  is null.");
            }
            var wine_Type = await _context.Wine_Types.FindAsync(id);
            if (wine_Type != null)
            {
                _context.Wine_Types.Remove(wine_Type);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Wine_TypeExists(int id)
        {
          return _context.Wine_Types.Any(e => e.ID == id);
        }
    }
}
