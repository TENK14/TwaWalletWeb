using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwaWallet.Entity.Migrations;
using TwaWallet.Model;

namespace TwaWallet.Entity
{
    //public class TwaWalletDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TwaWalletDbContext>
    public class TwaWalletDbInitializer : System.Data.Entity.CreateDatabaseIfNotExists<TwaWalletDbContext>
    //public class TwaWalletDbInitializer : System.Data.Entity.MigrateDatabaseToLatestVersion<TwaWalletDbContext, Configuration>
    {
        protected override void Seed(TwaWalletDbContext context)
        {
            /**/
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

            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var paymentTypes = new List<PaymentType>
            {
                new PaymentType { Description = "Hotovost", IsDefault = false },
                new PaymentType { Description = "Karta", IsDefault = true }
            };
            paymentTypes.ForEach(p => context.PaymentTypes.Add(p));
            context.SaveChanges();

            var intervals = new List<Interval>
            {
                new Interval { Description = "Denně", IntervalCode = "000001" , IsDefault = false },
                new Interval { Description = "Týdně", IntervalCode = "000007" ,IsDefault = false },
                new Interval { Description = "Měsíčně", IntervalCode = "000100" ,IsDefault = true },
                new Interval { Description = "Čtvrtletně", IntervalCode = "000300" ,IsDefault = false },
                new Interval { Description = "Ročně", IntervalCode = "010000" ,IsDefault = false },
            };

            intervals.ForEach(i => context.Intervals.Add(i));
            context.SaveChanges();
            /**/
        }        
    }
}
