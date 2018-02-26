using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwaWallet.Entity.Migrations;
using TwaWallet.Model;

namespace TwaWallet.Entity
{
    public class TwaWalletDbContext : DbContext
    {
        private new static bool AutoDetectChangesEnabled = true;

        static TwaWalletDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TwaWalletDbContext, Configuration>());
            //Database.SetInitializer<TwaWalletDbContext>(new TwaWalletDbInitializer());
        }

        public TwaWalletDbContext() : base("name=DefaultConnectionString")
        {
            this.Configuration.AutoDetectChangesEnabled = AutoDetectChangesEnabled;

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(this.GetType().Assembly);
            // TODO: Havit featura - zjisti, jak pak pracuji s DB, kdyz nejsou DbSety
            //modelBuilder.RegisterModelFromAssembly(typeof(User).Assembly); 
        }

        public System.Data.Entity.DbSet<TwaWallet.Model.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<TwaWallet.Model.Interval> Intervals { get; set; }

        public System.Data.Entity.DbSet<TwaWallet.Model.PaymentType> PaymentTypes { get; set; }

        public System.Data.Entity.DbSet<TwaWallet.Model.RecurringPayment> RecurringPayments { get; set; }

        public System.Data.Entity.DbSet<TwaWallet.Model.User> Users { get; set; }

        public System.Data.Entity.DbSet<TwaWallet.Model.Record> Records { get; set; }
    }
}
