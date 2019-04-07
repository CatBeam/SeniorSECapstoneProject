namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountJointPrimaryKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CustomerAccounts");
            AlterColumn("dbo.CustomerAccounts", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerAccounts", "AccountNum", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CustomerAccounts", new[] { "Id", "AccountNum" });
            DropPrimaryKey("dbo.CustomerAccounts");
            AddColumn("dbo.CustomerAccounts", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CustomerAccounts", new[] { "AccountNum", "UserId" });
            DropColumn("dbo.CustomerAccounts", "Id");
            DropTable("dbo.Customers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropPrimaryKey("dbo.CustomerAccounts");
            AlterColumn("dbo.CustomerAccounts", "AccountNum", c => c.String());
            AlterColumn("dbo.CustomerAccounts", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CustomerAccounts", "Id");
            AddColumn("dbo.CustomerAccounts", "Id", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.CustomerAccounts");
            DropColumn("dbo.CustomerAccounts", "UserId");
            AddPrimaryKey("dbo.CustomerAccounts", new[] { "Id", "AccountNum" });
        }
    }
}
