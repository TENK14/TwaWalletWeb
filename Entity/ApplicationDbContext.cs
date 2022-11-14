using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //builder.Conventions.Remove<PluralizingTableNameConvention>();
            //builder.Configurations.AddFromAssembly(this.GetType().Assembly);
            // TODO: Havit featura - zjisti, jak pak pracuji s DB, kdyz nejsou DbSety
            //modelBuilder.RegisterModelFromAssembly(typeof(User).Assembly); 

            // Category
            modelBuilder.Entity(typeof(Category))
            .HasOne(typeof(ApplicationUser), nameof(Category.ApplicationUser))
            .WithMany()
            .HasForeignKey(nameof(Category.ApplicationUserId))
            .OnDelete(DeleteBehavior.Restrict);

            // PaymentType
            modelBuilder.Entity(typeof(PaymentType))
            .HasOne(typeof(ApplicationUser), nameof(PaymentType.ApplicationUser))
            .WithMany()
            .HasForeignKey(nameof(PaymentType.ApplicationUserId))
            .OnDelete(DeleteBehavior.Restrict);

            // Record
            modelBuilder.Entity(typeof(Record))
            .HasOne(typeof(ApplicationUser), nameof(Record.ApplicationUser))
            .WithMany()
            .HasForeignKey(nameof(Record.ApplicationUserId))
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity(typeof(Record))
            .HasOne(typeof(Category), nameof(Record.Category))
            .WithMany()
            .HasForeignKey(nameof(Record.CategoryId))
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity(typeof(Record))
            .HasOne(typeof(PaymentType), nameof(Record.PaymentType))
            .WithMany()
            .HasForeignKey(nameof(Record.PaymentTypeId))
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity(typeof(Record))
                .HasIndex(nameof(Record.Description));

            modelBuilder.Entity(typeof(Record))
                .HasIndex(nameof(Record.Tag));

            modelBuilder.Entity(typeof(Record))
                .HasIndex(nameof(Record.Category.Description));

            modelBuilder.Entity(typeof(Record))
                .HasIndex(nameof(Record.Date));

            // RecurringPayment
            modelBuilder.Entity(typeof(RecurringPayment))
            .HasOne(typeof(ApplicationUser), nameof(RecurringPayment.ApplicationUser))
            .WithMany()
            .HasForeignKey(nameof(RecurringPayment.ApplicationUserId))
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity(typeof(RecurringPayment))
            .HasOne(typeof(Category), nameof(RecurringPayment.Category))
            .WithMany()
            .HasForeignKey(nameof(RecurringPayment.CategoryId))
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity(typeof(RecurringPayment))
            .HasOne(typeof(PaymentType), nameof(RecurringPayment.PaymentType))
            .WithMany()
            .HasForeignKey(nameof(RecurringPayment.PaymentTypeId))
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity(typeof(RecurringPayment))
            .HasOne(typeof(Interval), nameof(RecurringPayment.Interval))
            .WithMany()
            .HasForeignKey(nameof(RecurringPayment.IntervalId))
            .OnDelete(DeleteBehavior.Restrict);

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
