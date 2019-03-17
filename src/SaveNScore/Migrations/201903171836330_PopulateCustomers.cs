namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCustomers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Customers (FirstName, LastName) VALUES ('Steve', 'Kesteverson')");
            Sql("INSERT INTO Customers (FirstName, LastName) VALUES ('Karen', 'Smith')");
        }

        public override void Down()
        {
        }
    }
}
