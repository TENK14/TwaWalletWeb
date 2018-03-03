namespace TwaWallet.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Id_intToGuid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Record", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.RecurringPayment", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.RecurringPayment", "IntervalId", "dbo.Interval");
            DropForeignKey("dbo.Record", "PaymentTypeId", "dbo.PaymentType");
            DropForeignKey("dbo.RecurringPayment", "PaymentTypeId", "dbo.PaymentType");
            DropForeignKey("dbo.Record", "UserId", "dbo.User");
            DropForeignKey("dbo.RecurringPayment", "UserId", "dbo.User");
            DropIndex("dbo.Record", new[] { "CategoryId" });
            DropIndex("dbo.Record", new[] { "UserId" });
            DropIndex("dbo.Record", new[] { "PaymentTypeId" });
            DropIndex("dbo.RecurringPayment", new[] { "CategoryId" });
            DropIndex("dbo.RecurringPayment", new[] { "PaymentTypeId" });
            DropIndex("dbo.RecurringPayment", new[] { "UserId" });
            DropIndex("dbo.RecurringPayment", new[] { "IntervalId" });
            DropPrimaryKey("dbo.Category");
            DropPrimaryKey("dbo.Interval");
            DropPrimaryKey("dbo.PaymentType");
            DropPrimaryKey("dbo.Record");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.RecurringPayment");

            //AlterColumn("dbo.Category", "Id", c => c.Guid(nullable: false, identity: true));
            //AlterColumn("dbo.Interval", "Id", c => c.Guid(nullable: false, identity: true));
            //AlterColumn("dbo.PaymentType", "Id", c => c.Guid(nullable: false, identity: true));
            //AlterColumn("dbo.Record", "Id", c => c.Guid(nullable: false, identity: true));

            //AlterColumn("dbo.Record", "CategoryId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.Record", "UserId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.Record", "PaymentTypeId", c => c.Guid(nullable: false));

            //AlterColumn("dbo.User", "Id", c => c.Guid(nullable: false, identity: true));

            //AlterColumn("dbo.RecurringPayment", "Id", c => c.Guid(nullable: false, identity: true));

            //AlterColumn("dbo.RecurringPayment", "CategoryId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.RecurringPayment", "PaymentTypeId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.RecurringPayment", "UserId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.RecurringPayment", "IntervalId", c => c.Guid(nullable: false));

            //---------------------------------------------------------------------------------------
            DropColumn("dbo.Category", "Id");
            DropColumn("dbo.Interval", "Id");
            DropColumn("dbo.PaymentType", "Id");
            DropColumn("dbo.Record", "Id");
            DropColumn("dbo.Record", "CategoryId");
            DropColumn("dbo.Record", "UserId");
            DropColumn("dbo.Record", "PaymentTypeId");
            DropColumn("dbo.User", "Id");
            DropColumn("dbo.RecurringPayment", "Id");
            DropColumn("dbo.RecurringPayment", "CategoryId");
            DropColumn("dbo.RecurringPayment", "PaymentTypeId");
            DropColumn("dbo.RecurringPayment", "UserId");
            DropColumn("dbo.RecurringPayment", "IntervalId");

            //DropColumn("dbo.DownloadTokens", "Id");
            //AddColumn("dbo.DownloadTokens", "Id", c => c.Guid(nullable: false, identity: true));

            AddColumn("dbo.Category", "Id", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.Interval", "Id", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.PaymentType", "Id", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.Record", "Id", c => c.Guid(nullable: false, identity: true));
            
            AddColumn("dbo.Record", "CategoryId", c => c.Guid(nullable: false));
            AddColumn("dbo.Record", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.Record", "PaymentTypeId", c => c.Guid(nullable: false));
            
            AddColumn("dbo.User", "Id", c => c.Guid(nullable: false, identity: true));
            
            AddColumn("dbo.RecurringPayment", "Id", c => c.Guid(nullable: false, identity: true));
            
            AddColumn("dbo.RecurringPayment", "CategoryId", c => c.Guid(nullable: false));
            AddColumn("dbo.RecurringPayment", "PaymentTypeId", c => c.Guid(nullable: false));
            AddColumn("dbo.RecurringPayment", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.RecurringPayment", "IntervalId", c => c.Guid(nullable: false));
            //---------------------------------------------------------------------------------------


            AddPrimaryKey("dbo.Category", "Id");
            AddPrimaryKey("dbo.Interval", "Id");
            AddPrimaryKey("dbo.PaymentType", "Id");
            AddPrimaryKey("dbo.Record", "Id");
            AddPrimaryKey("dbo.User", "Id");
            AddPrimaryKey("dbo.RecurringPayment", "Id");
            CreateIndex("dbo.Record", "CategoryId");
            CreateIndex("dbo.Record", "UserId");
            CreateIndex("dbo.Record", "PaymentTypeId");
            CreateIndex("dbo.RecurringPayment", "CategoryId");
            CreateIndex("dbo.RecurringPayment", "PaymentTypeId");
            CreateIndex("dbo.RecurringPayment", "UserId");
            CreateIndex("dbo.RecurringPayment", "IntervalId");
            AddForeignKey("dbo.Record", "CategoryId", "dbo.Category", "Id", cascadeDelete: false);
            AddForeignKey("dbo.RecurringPayment", "CategoryId", "dbo.Category", "Id", cascadeDelete: false);
            AddForeignKey("dbo.RecurringPayment", "IntervalId", "dbo.Interval", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Record", "PaymentTypeId", "dbo.PaymentType", "Id", cascadeDelete: false);
            AddForeignKey("dbo.RecurringPayment", "PaymentTypeId", "dbo.PaymentType", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Record", "UserId", "dbo.User", "Id", cascadeDelete: false);
            AddForeignKey("dbo.RecurringPayment", "UserId", "dbo.User", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecurringPayment", "UserId", "dbo.User");
            DropForeignKey("dbo.Record", "UserId", "dbo.User");
            DropForeignKey("dbo.RecurringPayment", "PaymentTypeId", "dbo.PaymentType");
            DropForeignKey("dbo.Record", "PaymentTypeId", "dbo.PaymentType");
            DropForeignKey("dbo.RecurringPayment", "IntervalId", "dbo.Interval");
            DropForeignKey("dbo.RecurringPayment", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Record", "CategoryId", "dbo.Category");
            DropIndex("dbo.RecurringPayment", new[] { "IntervalId" });
            DropIndex("dbo.RecurringPayment", new[] { "UserId" });
            DropIndex("dbo.RecurringPayment", new[] { "PaymentTypeId" });
            DropIndex("dbo.RecurringPayment", new[] { "CategoryId" });
            DropIndex("dbo.Record", new[] { "PaymentTypeId" });
            DropIndex("dbo.Record", new[] { "UserId" });
            DropIndex("dbo.Record", new[] { "CategoryId" });
            DropPrimaryKey("dbo.RecurringPayment");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.Record");
            DropPrimaryKey("dbo.PaymentType");
            DropPrimaryKey("dbo.Interval");
            DropPrimaryKey("dbo.Category");
            //AlterColumn("dbo.RecurringPayment", "IntervalId", c => c.Int(nullable: false));
            //AlterColumn("dbo.RecurringPayment", "UserId", c => c.Int(nullable: false));
            //AlterColumn("dbo.RecurringPayment", "PaymentTypeId", c => c.Int(nullable: false));
            //AlterColumn("dbo.RecurringPayment", "CategoryId", c => c.Int(nullable: false));
            //AlterColumn("dbo.RecurringPayment", "Id", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.User", "Id", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.Record", "PaymentTypeId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Record", "UserId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Record", "CategoryId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Record", "Id", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.PaymentType", "Id", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.Interval", "Id", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.Category", "Id", c => c.Int(nullable: false, identity: true));

            //---------------------------------------------------------------------------------------
            DropColumn("dbo.Category", "Id");
            DropColumn("dbo.Interval", "Id");
            DropColumn("dbo.PaymentType", "Id");
            DropColumn("dbo.Record", "Id");
            DropColumn("dbo.Record", "CategoryId");
            DropColumn("dbo.Record", "UserId");
            DropColumn("dbo.Record", "PaymentTypeId");
            DropColumn("dbo.User", "Id");
            DropColumn("dbo.RecurringPayment", "Id");
            DropColumn("dbo.RecurringPayment", "CategoryId");
            DropColumn("dbo.RecurringPayment", "PaymentTypeId");
            DropColumn("dbo.RecurringPayment", "UserId");
            DropColumn("dbo.RecurringPayment", "IntervalId");

            //DropColumn("dbo.DownloadTokens", "Id");
            //AddColumn("dbo.DownloadTokens", "Id", c => c.Guid(nullable: false, identity: true));

            AddColumn("dbo.Category", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Interval", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.PaymentType", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Record", "Id", c => c.Int(nullable: false, identity: true));

            AddColumn("dbo.Record", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Record", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Record", "PaymentTypeId", c => c.Int(nullable: false));

            AddColumn("dbo.User", "Id", c => c.Int(nullable: false, identity: true));

            AddColumn("dbo.RecurringPayment", "Id", c => c.Int(nullable: false, identity: true));

            AddColumn("dbo.RecurringPayment", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.RecurringPayment", "PaymentTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.RecurringPayment", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.RecurringPayment", "IntervalId", c => c.Int(nullable: false));
            //---------------------------------------------------------------------------------------

            AddPrimaryKey("dbo.RecurringPayment", "Id");
            AddPrimaryKey("dbo.User", "Id");
            AddPrimaryKey("dbo.Record", "Id");
            AddPrimaryKey("dbo.PaymentType", "Id");
            AddPrimaryKey("dbo.Interval", "Id");
            AddPrimaryKey("dbo.Category", "Id");
            CreateIndex("dbo.RecurringPayment", "IntervalId");
            CreateIndex("dbo.RecurringPayment", "UserId");
            CreateIndex("dbo.RecurringPayment", "PaymentTypeId");
            CreateIndex("dbo.RecurringPayment", "CategoryId");
            CreateIndex("dbo.Record", "PaymentTypeId");
            CreateIndex("dbo.Record", "UserId");
            CreateIndex("dbo.Record", "CategoryId");
            AddForeignKey("dbo.RecurringPayment", "UserId", "dbo.User", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Record", "UserId", "dbo.User", "Id", cascadeDelete: false);
            AddForeignKey("dbo.RecurringPayment", "PaymentTypeId", "dbo.PaymentType", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Record", "PaymentTypeId", "dbo.PaymentType", "Id", cascadeDelete: false);
            AddForeignKey("dbo.RecurringPayment", "IntervalId", "dbo.Interval", "Id", cascadeDelete: false);
            AddForeignKey("dbo.RecurringPayment", "CategoryId", "dbo.Category", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Record", "CategoryId", "dbo.Category", "Id", cascadeDelete: false);
        }
    }
}
