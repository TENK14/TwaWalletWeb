namespace TwaWallet.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AspNetUsers : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LoginAccount", newName: "AspNetUsers");
            DropForeignKey("dbo.Record", "LoginAccountId", "dbo.LoginAccount");
            DropForeignKey("dbo.RecurringPayment", "LoginAccountId", "dbo.LoginAccount");
            DropIndex("dbo.Record", new[] { "LoginAccountId" });
            DropIndex("dbo.RecurringPayment", new[] { "LoginAccountId" });
            DropPrimaryKey("dbo.AspNetUsers");
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            AddColumn("dbo.AspNetUsers", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccessFailedCount", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Record", "LoginAccountId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.RecurringPayment", "LoginAccountId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.AspNetUsers", "Id");
            CreateIndex("dbo.Record", "LoginAccountId");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.RecurringPayment", "LoginAccountId");
            AddForeignKey("dbo.Record", "LoginAccountId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RecurringPayment", "LoginAccountId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecurringPayment", "LoginAccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Record", "LoginAccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RecurringPayment", new[] { "LoginAccountId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Record", new[] { "LoginAccountId" });
            DropPrimaryKey("dbo.AspNetUsers");
            AlterColumn("dbo.RecurringPayment", "LoginAccountId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Record", "LoginAccountId", c => c.Guid(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Id", c => c.Guid(nullable: false, identity: true));
            DropColumn("dbo.AspNetUsers", "AccessFailedCount");
            DropColumn("dbo.AspNetUsers", "LockoutEnabled");
            DropColumn("dbo.AspNetUsers", "LockoutEndDateUtc");
            DropColumn("dbo.AspNetUsers", "TwoFactorEnabled");
            DropColumn("dbo.AspNetUsers", "PhoneNumberConfirmed");
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "SecurityStamp");
            DropColumn("dbo.AspNetUsers", "PasswordHash");
            DropColumn("dbo.AspNetUsers", "EmailConfirmed");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            AddPrimaryKey("dbo.AspNetUsers", "Id");
            CreateIndex("dbo.RecurringPayment", "LoginAccountId");
            CreateIndex("dbo.Record", "LoginAccountId");
            AddForeignKey("dbo.RecurringPayment", "LoginAccountId", "dbo.LoginAccount", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Record", "LoginAccountId", "dbo.LoginAccount", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.AspNetUsers", newName: "LoginAccount");
        }
    }
}
