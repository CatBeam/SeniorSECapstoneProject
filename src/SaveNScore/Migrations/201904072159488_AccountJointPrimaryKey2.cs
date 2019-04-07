namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountJointPrimaryKey2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CustomerAccounts");
            AddColumn("dbo.CustomerAccounts", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CustomerAccounts", new[] { "AccountNum", "UserId" });
            DropColumn("dbo.CustomerAccounts", "Id");
            DropTable("dbo.Customers");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerAccounts", "Id", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.CustomerAccounts");
            DropColumn("dbo.CustomerAccounts", "UserId");
            AddPrimaryKey("dbo.CustomerAccounts", new[] { "Id", "AccountNum" });
        }
    }
}
