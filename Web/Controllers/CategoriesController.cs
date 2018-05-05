using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TwaWallet.Entity;
using TwaWallet.Model;

namespace TwaWallet.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class CategoriesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context,
             UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            //_signInManager = signInManager;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            // TODO: implementuj, ze kontext, bude vracet pouze uzivatelska data
            var applicationDbContext = await GetUsersCategoriesAsync();
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await GetUsersCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,IsDefault,ApplicationUserId,Id")] Category categoryIM)
        //public async Task<IActionResult> Create([Bind("Description,IsDefault,Id")] Category category)
        {               
            if (ModelState.IsValid
                || (ModelState.ErrorCount == 1 
                    && ModelState.GetValidationState(nameof(Category.ApplicationUserId)) == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    )
            {
                var user = await _userManager.GetUserAsync(User);

                categoryIM.ApplicationUser = user;

                _context.Add(categoryIM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", categoryIM.ApplicationUserId);
            return View(categoryIM);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await GetUsersCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", category.ApplicationUserId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Description,IsDefault,ApplicationUserId,Id")] Category categoryIM)
        {
            Category category = null;

            if ( String.IsNullOrWhiteSpace(id)
                || (categoryIM == null)
                || (id != categoryIM.Id) )
            {
                return NotFound();
            }

            if (ModelState.IsValid
                || (ModelState.ErrorCount == 1
                    && ModelState.GetValidationState(nameof(Category.ApplicationUserId)) == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    )
            {
                try
                {
                    category = await GetUsersCategoryAsync(id);

                    if (category == null)
                    {
                        return NotFound();
                    }
                                        
                    if (categoryIM.IsDefault)
                    {
                        var categoryDefault = (await GetUsersCategoriesAsync()).SingleOrDefault(c => c.IsDefault);
                        categoryDefault.IsDefault = false;
                        _context.Update(categoryDefault);
                    }

                    category.Description = categoryIM.Description;
                    category.IsDefault = categoryIM.IsDefault;

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(categoryIM.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", categoryIM.ApplicationUserId);
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await GetUsersCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var category = await GetUsersCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(string id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        private async Task<Category> GetUsersCategoryAsync(string id)
        {
            var user = await _userManager.GetUserAsync(User);

            return await _context.Categories.SingleOrDefaultAsync(m => m.Id == id && m.ApplicationUser == user);
        }

        private async Task<IQueryable<Category>> GetUsersCategoriesAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            return _context.Categories.Where(c => c.ApplicationUser == user);
        }
    }
}
