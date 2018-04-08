using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwaWallet.Model;

namespace TwaWallet.Entity
{
    public static class ApplicationDbContextExtensions
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            //if (context.AllMigrationsApplied())
            if (!context.Database.GetPendingMigrations().Any())
                {
                if (!context.Intervals.Any())
                {
                    var intervals = new List<Interval>
                    {
                        new Interval { Description = "Denně", IntervalCode = "000001" , IsDefault = false },
                        new Interval { Description = "Týdně", IntervalCode = "000007" ,IsDefault = false },
                        new Interval { Description = "Měsíčně", IntervalCode = "000100" ,IsDefault = true },
                        new Interval { Description = "Čtvrtletně", IntervalCode = "000300" ,IsDefault = false },
                        new Interval { Description = "Ročně", IntervalCode = "010000" ,IsDefault = false },
                    };

                    context.Intervals.AddRange(intervals);
                    context.SaveChanges();
                }
            }
        }

        public static void NewApplicationUserSeedData(this ApplicationDbContext context, ApplicationUser user)
        {
            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.Categories.Any(c => c.ApplicationUser == user))
                {
                    var categories = new List<Category>
                    {
                        new Category { Description = "Obědy", IsDefault = true },
                        new Category { Description = "Výlety", IsDefault = false },
                        new Category { Description = "Potraviny", IsDefault = false },
                        new Category { Description = "Nezařazené", IsDefault = false },
                        new Category { Description = "Restaurace, bary", IsDefault = false },
                        new Category { Description = "Domácnost", IsDefault = false },
                        new Category { Description = "Oblečení", IsDefault = false },
                        new Category { Description = "Pohonné hmoty", IsDefault = false },
                        new Category { Description = "Služby", IsDefault = false },
                        new Category { Description = "Drogerie", IsDefault = false },
                        new Category { Description = "Mzda", IsDefault = false },
                        new Category { Description = "Léky", IsDefault = false },
                        new Category { Description = "Zábava, kultura", IsDefault = false },
                        new Category { Description = "Sport", IsDefault = false },
                        new Category { Description = "Doprava", IsDefault = false },
                    };

                    context.Categories.AddRange(categories.Select(c => { c.ApplicationUser = user; return c; }));
                }

                if (!context.PaymentTypes.Any(c => c.ApplicationUser == user))
                {
                    var paymentTypes = new List<PaymentType>
                    {
                        new PaymentType { Description = "Hotovost", IsDefault = false },
                        new PaymentType { Description = "Karta", IsDefault = true }
                    };

                    context.PaymentTypes.AddRange(paymentTypes.Select(pt => {pt.ApplicationUser = user; return pt; }));
                }

                context.SaveChanges();
            }
        }
    }
}
