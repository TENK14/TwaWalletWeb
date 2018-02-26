namespace TwaWallet.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Record_Description_nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Record", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Record", "Description", c => c.String(nullable: false));
        }
    }
}
