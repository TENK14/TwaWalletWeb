namespace TwaWallet.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserToLoginAccount : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Record", "UserId", "dbo.User");
            DropForeignKey("dbo.RecurringPayment", "UserId", "dbo.User");
            DropIndex("dbo.Record", new[] { "UserId" });
            DropIndex("dbo.RecurringPayment", new[] { "UserId" });
            CreateTable(
                "dbo.LoginAccount",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        VCode = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Record", "LoginAccountId", c => c.Guid(nullable: false));
            AddColumn("dbo.RecurringPayment", "LoginAccountId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Record", "LoginAccountId");
            CreateIndex("dbo.RecurringPayment", "LoginAccountId");
            AddForeignKey("dbo.Record", "LoginAccountId", "dbo.LoginAccount", "Id", cascadeDelete: false);
            AddForeignKey("dbo.RecurringPayment", "LoginAccountId", "dbo.LoginAccount", "Id", cascadeDelete: false);
            DropColumn("dbo.Record", "UserId");
            DropColumn("dbo.RecurringPayment", "UserId");
            DropTable("dbo.User");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RecurringPayment", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.Record", "UserId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.RecurringPayment", "LoginAccountId", "dbo.LoginAccount");
            DropForeignKey("dbo.Record", "LoginAccountId", "dbo.LoginAccount");
            DropIndex("dbo.RecurringPayment", new[] { "LoginAccountId" });
            DropIndex("dbo.Record", new[] { "LoginAccountId" });
            DropColumn("dbo.RecurringPayment", "LoginAccountId");
            DropColumn("dbo.Record", "LoginAccountId");
            DropTable("dbo.LoginAccount");
            CreateIndex("dbo.RecurringPayment", "UserId");
            CreateIndex("dbo.Record", "UserId");
            AddForeignKey("dbo.RecurringPayment", "UserId", "dbo.User", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Record", "UserId", "dbo.User", "Id", cascadeDelete: false);
        }
    }
}
