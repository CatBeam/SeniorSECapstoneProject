namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransDescription = c.String(nullable: false),
                        SpendingCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransactionCategories");
        }
    }
}
