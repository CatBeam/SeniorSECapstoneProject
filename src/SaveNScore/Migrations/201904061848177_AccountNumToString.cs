namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountNumToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerTransactions", "AccountNum", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerTransactions", "AccountNum", c => c.Int(nullable: false));
        }
    }
}
