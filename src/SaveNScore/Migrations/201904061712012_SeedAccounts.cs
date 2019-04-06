namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAccounts : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CustomerAccounts(AccountNum, CustomerAccountType, Balance) VALUES('211111110', '0', '239.00')");
            Sql("INSERT INTO CustomerAccounts(AccountNum, CustomerAccountType, Balance) VALUES('3111 3450 2930 9203', '2', '500.00')");
            Sql("INSERT INTO CustomerAccounts(AccountNum, CustomerAccountType, Balance) VALUES('3011111130', '1', '872.00')");
            Sql("INSERT INTO CustomerAccounts(AccountNum, CustomerAccountType, Balance) VALUES('411111111', '0', '5340.28')");
            Sql("INSERT INTO CustomerAccounts(AccountNum, CustomerAccountType, Balance) VALUES('8222222228', '1', '0.00')");
        }
        
        public override void Down()
        {
        }
    }
}
