namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorTransaction : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Transactions", newName: "CustomerTransactions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CustomerTransactions", newName: "Transactions");
        }
    }
}
