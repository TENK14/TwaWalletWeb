using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwaWallet.Entity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TwaWalletDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = false;
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TwaWalletDbContext context)
        {
            // nop
        }
    }
}
