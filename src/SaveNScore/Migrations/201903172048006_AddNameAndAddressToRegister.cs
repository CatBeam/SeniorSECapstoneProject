namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameAndAddressToRegister : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "AddressStreet", c => c.String());
            AddColumn("dbo.AspNetUsers", "AddressCity", c => c.String());
            AddColumn("dbo.AspNetUsers", "AddressState", c => c.String());
            AddColumn("dbo.AspNetUsers", "AddressZipCode", c => c.String());
            AlterColumn("dbo.Customers", "FirstName", c => c.String());
            AlterColumn("dbo.Customers", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "LastName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Customers", "FirstName", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.AspNetUsers", "AddressZipCode");
            DropColumn("dbo.AspNetUsers", "AddressState");
            DropColumn("dbo.AspNetUsers", "AddressCity");
            DropColumn("dbo.AspNetUsers", "AddressStreet");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
