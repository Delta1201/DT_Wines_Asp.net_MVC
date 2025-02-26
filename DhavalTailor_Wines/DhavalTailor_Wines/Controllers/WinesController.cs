using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DhavalTailor_Wines.Data;
using DhavalTailor_Wines.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Azure;
using static System.Net.Mime.MediaTypeNames;

namespace DhavalTailor_Wines.Controllers
{
    public class WinesController : Controller
    {
        private readonly WinesContext _context;

        public WinesController(WinesContext context)
        {
            _context = context;
        }

        // GET: Wines //code added here for sorting and filtering, all the parameters should match with class
        public async Task<IActionResult> Index(int? wine_TypeID, string wineName, 
            string sortDirectionCheck, string actionButton, 
            string sortDirection = "asc", string sortField = "Wine Type")
        {
            //Toggle the Open/Closed state of the collapse depending on if we are filtering
            //this creates session sort off
            ViewData["Filtering"] = "btn-outline-primary"; //Assume not filtering
            //Then in each "test" for filtering, add ViewData["Filtering"] = " show" if true;

            // Define the available sort options
            string[] sortOptions = new[] { "Wine Name", "Wine Year", "Wine Price", "Wine Type" };

            // Initialize the query
            var winesContext = _context.Wines.Include(w => w.Wine_Type).AsQueryable();
            //ToListAsync();
            //AsQueryable();

            // Check if a filter has been applied
            if (wine_TypeID.HasValue)
            {
                winesContext = winesContext.Where(w => w.Wine_TypeID == wine_TypeID);
                ViewData["Filtering"] = "btn-danger";
            }

            if (!string.IsNullOrEmpty(wineName))
            {
                winesContext = winesContext.Where(w => w.WineName.ToUpper().Contains(wineName.ToUpper()));
                ViewData["Filtering"] = "btn-danger";
            }

            //Before we sort, see if we have called for a change of filtering or sorting
            if (!String.IsNullOrEmpty(actionButton)) //Form Submitted!
            {
                //page = 1;//Reset page to start
                if (sortOptions.Contains(actionButton))//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
                else //Sort by the controls in the filter area
                {
                    sortDirection = String.IsNullOrEmpty(sortDirectionCheck) ? "asc" : "desc";
                    //sortField = sortFieldID;
                }
            }

            // Apply sorting
            if (sortOptions.Contains(sortField))
            {
                if (sortDirection == "asc")
                {
                    if (sortField == "Wine Name")
                    {
                        winesContext = winesContext.OrderBy(w => w.WineName);
                    }
                    else if (sortField == "Wine Year")
                    {
                        winesContext = winesContext.OrderBy(w => w.WineYear);
                    }
                    else if (sortField == "Wine Price")
                    {
                        winesContext = winesContext.OrderBy(w => w.WinePrice);
                    }
                    
                    else if (sortField == "Wine Type")
                    {
                        winesContext = winesContext.OrderBy(w => w.Wine_Type.WineTypeName);
                    }
                }
                else
                {
                    if (sortField == "Wine Name")
                    {
                        winesContext = winesContext.OrderByDescending(w => w.WineName);
                    }
                    else if (sortField == "Wine Year")
                    {
                        winesContext = winesContext.OrderByDescending(w => w.WineYear);
                    }
                    else if (sortField == "Wine Price")
                    {
                        winesContext = winesContext.OrderByDescending(w => w.WinePrice);
                    }
                    else if (sortField == "Wine Type")
                    {
                        winesContext = winesContext.OrderByDescending(w => w.Wine_Type.WineTypeName);
                    }
                }
            }

            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;
            //SelectList for Sorting Options
            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            // Set the SelectList in ViewData=this is to populate the dropdown list
            ViewData["Wine_TypeID"] = new SelectList(_context.Wine_Types, "ID", "WineTypeName");
            
            return View(await winesContext.ToListAsync());
        }

        // GET: Wines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Wines == null)
            {
                return NotFound();
            }

            var wine = await _context.Wines
                .Include(w => w.Wine_Type)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wine == null)
            {
                return NotFound();
            }

            return View(wine);
        }

        // GET: Wines/Create
        public IActionResult Create()
        {
            ViewData["Wine_TypeID"] = new SelectList(_context.Wine_Types, "ID", "WineTypeName");
            return View();
        }

        // POST: Wines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,WineName,WineYear,WinePrice,WineHarvest,Wine_TypeID")] Wine wine)
        {
            if (ModelState.IsValid)
            {
                // Check if a wine with the same name and year already exists
                var existingWine = _context.Wines
                    .FirstOrDefault(w => w.WineName == wine.WineName && w.WineYear == wine.WineYear);

                if (existingWine != null)
                {
                    ModelState.AddModelError(string.Empty, "A wine with the same name and year already exists.");
                    ViewData["Wine_TypeID"] = new SelectList(_context.Wine_Types, "ID", "WineTypeName", wine.Wine_TypeID);
                    return View(wine);
                }

                // If no duplicate is found, save the new wine
                _context.Add(wine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Wine_TypeID"] = new SelectList(_context.Wine_Types, "ID", "WineTypeName", wine.Wine_TypeID);
            return View(wine);
        }


        // GET: Wines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Wines == null)
            {
                return NotFound();
            }

            var wine = await _context.Wines.FindAsync(id);
            //var wine = await _context.Wines.Include(m => m.Wine_Type).FirstOrDefaultAsync(m => m.ID == id);
                //(FirstOrDefaultAsync(m => m.ID == id);
            if (wine == null)
            {
                return NotFound();
            }
            ViewData["Wine_TypeID"] = new SelectList(_context.Wine_Types, "ID", "WineTypeName", wine.Wine_TypeID);
            return View(wine);
        }

        // POST: Wines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,WineName,WineYear,WinePrice,WineHarvest,Wine_TypeID")] Wine wine)
        {
            if (id != wine.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WineExists(wine.ID))
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
            ViewData["Wine_TypeID"] = new SelectList(_context.Wine_Types, "ID", "WineTypeName", wine.Wine_TypeID);

            return View(wine);
        }

        // GET: Wines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var wineType = _context.Wine_Types.Find(id);
            if (id == null || _context.Wines == null)
            {
                return NotFound();
            }

            var wine = await _context.Wines
                .Include(w => w.Wine_Type)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wine == null)
            {
                return NotFound();
            }

            // There are wines of this type, prevent deletion
            var winesOfThisType = await _context.Wines
            .Where(w => w.Wine_TypeID == id)
            .ToListAsync();

            if (winesOfThisType.Any())
            {
                ModelState.AddModelError(string.Empty, "Cannot delete this Wine Type because there are wines of this type in the system.");
                return View(wineType);
            }

            return View(wine);
        }

        // POST: Wines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Wines == null)
            {
                return Problem("Entity set 'WinesContext.Wines'  is null.");
            }
            var wine = await _context.Wines.FindAsync(id);
            if (wine != null)
            {
                _context.Wines.Remove(wine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WineExists(int id)
        {
          return _context.Wines.Any(e => e.ID == id);
        }
    }
}
