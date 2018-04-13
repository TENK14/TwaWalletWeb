using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwaWallet.Entity;
using TwaWallet.Model;

namespace TwaWallet.Web.DataLayer
{
    // TODO: vymysli, jak tu dostat nejak rozumne Usera.
    //public class DataLayer
    //{
    //    private readonly ApplicationDbContext _context;
    //    private readonly UserManager<ApplicationUser> _userManager;

    //    public DataLayer(ApplicationDbContext context,
    //         UserManager<ApplicationUser> userManager)
    //    {
    //        this._context = context;
    //        this._userManager = userManager;
    //    }
    //    public bool CategoryExists(string id)
    //    {
    //        return _context.Categories.Any(e => e.Id == id);
    //    }

    //    public async Task<Category> GetUsersCategoryAsync(string id)
    //    {
    //        var user = await _userManager.GetUserAsync(User);

    //        return await _context.Categories.SingleOrDefaultAsync(m => m.Id == id && m.ApplicationUser == user);
    //    }

    //    public async Task<IQueryable<Category>> GetUsersCategoriesAsync()
    //    {
    //        var user = await _userManager.GetUserAsync(User);

    //        return _context.Categories.Where(c => c.ApplicationUser == user);
    //    }
    //}
}
