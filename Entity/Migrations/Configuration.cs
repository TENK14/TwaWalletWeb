using System.Collections.Generic;
//using System.Data.Entity.Migrations;
using System.Linq;
using TwaWallet.Model;

namespace TwaWallet.Entity.Migrations
{
    //internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    //{
    //    private readonly bool isInitialCreate;
    //    public Configuration()
    //    {
    //        AutomaticMigrationDataLossAllowed = false;
    //        AutomaticMigrationsEnabled = false;

    //        // http://blog.evizija.si/entity-framework-code-first-migrations/
    //        var migrator = new DbMigrator(this);
    //        isInitialCreate = migrator.GetPendingMigrations().Any(x => x.Contains("Init"));
    //    }

    //    protected override void Seed(TwaWalletDbContext context)
    //    {
    //        // nop
    //        /**/
    //        //if (context.Database.Exists())
    //        if (!isInitialCreate)
    //        {
    //            return;
    //        }

    //        var categories = new List<Category>
    //        {
    //            new Category { Description = "Obědy", IsDefault = true },
    //            new Category { Description = "Výlety", IsDefault = false },
    //            new Category { Description = "Potraviny", IsDefault = false },
    //            new Category { Description = "Nezařazené", IsDefault = false },
    //            new Category { Description = "Restaurace, bary", IsDefault = false },
    //            new Category { Description = "Domácnost", IsDefault = false },
    //            new Category { Description = "Oblečení", IsDefault = false },
    //            new Category { Description = "Pohonné hmoty", IsDefault = false },
    //            new Category { Description = "Služby", IsDefault = false },
    //            new Category { Description = "Drogerie", IsDefault = false },
    //            new Category { Description = "Mzda", IsDefault = false },
    //            new Category { Description = "Léky", IsDefault = false },
    //            new Category { Description = "Zábava, kultura", IsDefault = false },
    //            new Category { Description = "Sport", IsDefault = false },
    //            new Category { Description = "Doprava", IsDefault = false },
    //        };

    //        categories.ForEach(c => context.Categories.Add(c));
    //        context.SaveChanges();

    //        var paymentTypes = new List<PaymentType>
    //        {
    //            new PaymentType { Description = "Hotovost", IsDefault = false },
    //            new PaymentType { Description = "Karta", IsDefault = true }
    //        };
    //        paymentTypes.ForEach(p => context.PaymentTypes.Add(p));
    //        context.SaveChanges();

    //        var intervals = new List<Interval>
    //        {
    //            new Interval { Description = "Denně", IntervalCode = "000001" , IsDefault = false },
    //            new Interval { Description = "Týdně", IntervalCode = "000007" ,IsDefault = false },
    //            new Interval { Description = "Měsíčně", IntervalCode = "000100" ,IsDefault = true },
    //            new Interval { Description = "Čtvrtletně", IntervalCode = "000300" ,IsDefault = false },
    //            new Interval { Description = "Ročně", IntervalCode = "010000" ,IsDefault = false },
    //        };

    //        intervals.ForEach(i => context.Intervals.Add(i));
    //        context.SaveChanges();
    //        /**/
    //    }
    //}
}
