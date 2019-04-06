namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerTransactions", "Description", c => c.String());
            DropColumn("dbo.CustomerTransactions", "Year");
            DropColumn("dbo.CustomerTransactions", "Month");
            DropColumn("dbo.CustomerTransactions", "Day");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerTransactions", "Day", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerTransactions", "Month", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerTransactions", "Year", c => c.Int(nullable: false));
            DropColumn("dbo.CustomerTransactions", "Description");
        }
    }
}
