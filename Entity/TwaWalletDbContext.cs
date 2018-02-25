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
        }

        public TwaWalletDbContext() : base("name=DefaultConnectionString")
        {
            this.Configuration.AutoDetectChangesEnabled = AutoDetectChangesEnabled;
        }

        //public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(this.GetType().Assembly);
            modelBuilder.RegisterModelFromAssembly(typeof(User).Assembly);
        }

    }
}
