namespace TwaWallet.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Interval",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IntervalCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Record",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cost = c.Single(nullable: false),
                        Description = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Warranty = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        PaymentTypeId = c.Int(nullable: false),
                        Tag = c.String(),
                        Earnings = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentType", t => t.PaymentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.UserId)
                .Index(t => t.PaymentTypeId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecurringPayment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        PaymentTypeId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Cost = c.Single(nullable: false),
                        IntervalId = c.Int(nullable: false),
                        Earnings = c.Boolean(nullable: false),
                        Warranty = c.Int(nullable: false),
                        Tag = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Interval", t => t.IntervalId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentType", t => t.PaymentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.PaymentTypeId)
                .Index(t => t.UserId)
                .Index(t => t.IntervalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecurringPayment", "UserId", "dbo.User");
            DropForeignKey("dbo.RecurringPayment", "PaymentTypeId", "dbo.PaymentType");
            DropForeignKey("dbo.RecurringPayment", "IntervalId", "dbo.Interval");
            DropForeignKey("dbo.RecurringPayment", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Record", "UserId", "dbo.User");
            DropForeignKey("dbo.Record", "PaymentTypeId", "dbo.PaymentType");
            DropForeignKey("dbo.Record", "CategoryId", "dbo.Category");
            DropIndex("dbo.RecurringPayment", new[] { "IntervalId" });
            DropIndex("dbo.RecurringPayment", new[] { "UserId" });
            DropIndex("dbo.RecurringPayment", new[] { "PaymentTypeId" });
            DropIndex("dbo.RecurringPayment", new[] { "CategoryId" });
            DropIndex("dbo.Record", new[] { "PaymentTypeId" });
            DropIndex("dbo.Record", new[] { "UserId" });
            DropIndex("dbo.Record", new[] { "CategoryId" });
            DropTable("dbo.RecurringPayment");
            DropTable("dbo.User");
            DropTable("dbo.Record");
            DropTable("dbo.PaymentType");
            DropTable("dbo.Interval");
            DropTable("dbo.Category");
        }
    }
}
