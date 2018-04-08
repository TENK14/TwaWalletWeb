﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwaWallet.Model;

namespace TwaWallet.Entity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //builder.Conventions.Remove<PluralizingTableNameConvention>();
            //builder.Configurations.AddFromAssembly(this.GetType().Assembly);
            // TODO: Havit featura - zjisti, jak pak pracuji s DB, kdyz nejsou DbSety
            //modelBuilder.RegisterModelFromAssembly(typeof(User).Assembly); 
        }

        /// <summary>
        /// Pokud by bylo zadáno bez AppDomain: DbSet<Category>,
        /// tak nebude fungovat Scaffolding.
        /// </summary>
        public DbSet<TwaWallet.Model.Category> Categories { get; set; }
        public DbSet<TwaWallet.Model.Interval> Intervals { get; set; }
        public DbSet<TwaWallet.Model.PaymentType> PaymentTypes { get; set; }
        public DbSet<TwaWallet.Model.RecurringPayment> RecurringPayments { get; set; }
        public DbSet<TwaWallet.Model.Record> Records { get; set; }
    }
}
