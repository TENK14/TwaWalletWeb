using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TwaWallet.Entity;
using TwaWallet.Model;

namespace Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class PaymentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentTypesController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        // GET: PaymentTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await GetUsersPaymentTypes();

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PaymentTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await GetUsersPaymentType(id);

            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // GET: PaymentTypes/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: PaymentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,IsDefault,ApplicationUserId,Id")] PaymentType paymentType)
        {
            var user = await _userManager.GetUserAsync(User);
            paymentType.ApplicationUser = user;

            if (ModelState.IsValid
                || (ModelState.ErrorCount == 1
                    && ModelState.GetValidationState(nameof(Category.ApplicationUserId)) == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    )
            {
                _context.Add(paymentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", paymentType.ApplicationUserId);
            return View(paymentType);
        }

        // GET: PaymentTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await GetUsersPaymentType(id);
            if (paymentType == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", paymentType.ApplicationUserId);
            return View(paymentType);
        }

        // POST: PaymentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Description,IsDefault,ApplicationUserId,Id")] PaymentType paymentTypeIM)
        {
            PaymentType paymentType = null;

            if ( String.IsNullOrWhiteSpace(id)
                || (paymentTypeIM == null)
                || (id != paymentTypeIM.Id) )
            {
                return NotFound();
            }
            
            if (ModelState.IsValid
                || (ModelState.ErrorCount == 1
                    && ModelState.GetValidationState(nameof(PaymentType.ApplicationUserId)) == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    )
            {
                try
                {
                    paymentType = await GetUsersPaymentType(paymentTypeIM.Id);

                    if (paymentType == null)
                    {
                        return NotFound();
                    }

                    if (paymentTypeIM.IsDefault)
                    {
                        PaymentType defaultPaymentType = (await GetUsersPaymentTypes()).SingleOrDefault(pt => pt.IsDefault);
                        defaultPaymentType.IsDefault = false;
                        _context.Update(defaultPaymentType);
                    }

                    paymentType.Description = paymentTypeIM.Description;
                    paymentType.IsDefault = paymentTypeIM.IsDefault;

                    _context.Update(paymentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentTypeExists(paymentTypeIM.Id))
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
            
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", paymentTypeIM.ApplicationUserId);
            return View(paymentTypeIM);
        }

        // GET: PaymentTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var paymentType = await GetUsersPaymentType(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var paymentType = await GetUsersPaymentType(id);

            if (paymentType == null)
            {
                return NotFound();
            }

            _context.PaymentTypes.Remove(paymentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentTypeExists(string id)
        {
            return _context.PaymentTypes.Any(e => e.Id == id);
        }

        private async Task<PaymentType> GetUsersPaymentType(string id)
        {
            var user = await _userManager.GetUserAsync(User);

            return await _context.PaymentTypes.SingleOrDefaultAsync(pt => pt.Id == id && pt.ApplicationUser == user);
        }

        private async Task<IQueryable<PaymentType>> GetUsersPaymentTypes()
        {
            var user = await _userManager.GetUserAsync(User);

            return _context.PaymentTypes.Where(pt => pt.ApplicationUser == user);
        }
    }
}
