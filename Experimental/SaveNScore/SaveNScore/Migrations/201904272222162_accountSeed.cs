namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class accountSeed : DbMigration
    {
        public override void Up()
        {
            // account 1, all test users can see it
            Sql("INSERT INTO CustomerAccounts(UserID, AccountNum, AccountType, Balance) VALUES('4b2911f3-d129-4275-911a-41a2b2de8f6d', '211111110', '0', '239.00')");
            Sql("INSERT INTO CustomerAccounts(UserID, AccountNum, AccountType, Balance) VALUES('53f2c5a5-2ee2-4ddc-a344-460d848ff52b', '211111110', '0', '239.00')");
            Sql("INSERT INTO CustomerAccounts(UserID, AccountNum, AccountType, Balance) VALUES('df796b37-38ca-4d13-a6a9-a1f9a7947ba5', '211111110', '0', '239.00')");

            // account 2, all users can see it
            Sql("INSERT INTO CustomerAccounts(UserID, AccountNum, AccountType, Balance) VALUES('4b2911f3-d129-4275-911a-41a2b2de8f6d', '3011111130', '1', '872.00')");
            Sql("INSERT INTO CustomerAccounts(UserID, AccountNum, AccountType, Balance) VALUES('53f2c5a5-2ee2-4ddc-a344-460d848ff52b', '3011111130', '1', '872.00')");
            Sql("INSERT INTO CustomerAccounts(UserID, AccountNum, AccountType, Balance) VALUES('df796b37-38ca-4d13-a6a9-a1f9a7947ba5', '3011111130', '1', '872.00')");

            // account 3, all users can see it
            Sql("INSERT INTO CustomerAccounts(UserID, AccountNum, AccountType, Balance) VALUES('4b2911f3-d129-4275-911a-41a2b2de8f6d', '3111 3450 2930 9203', '2', '500.00')");
            Sql("INSERT INTO CustomerAccounts(UserID, AccountNum, AccountType, Balance) VALUES('53f2c5a5-2ee2-4ddc-a344-460d848ff52b', '3111 3450 2930 9203', '2', '500.00')");
            Sql("INSERT INTO CustomerAccounts(UserID, AccountNum, AccountType, Balance) VALUES('df796b37-38ca-4d13-a6a9-a1f9a7947ba5', '3111 3450 2930 9203', '2', '500.00')");

            // account 4, only donald can see it
            Sql("INSERT INTO CustomerAccounts(UserID, AccountNum, AccountType, Balance) VALUES('4b2911f3-d129-4275-911a-41a2b2de8f6d', '411111111', '0', '5340.28')");

            // account 5, only donald can see it
            Sql("INSERT INTO CustomerAccounts(UserID, AccountNum, AccountType, Balance) VALUES('4b2911f3-d129-4275-911a-41a2b2de8f6d', '8222222228', '1', '0.00')");
        }

        public override void Down()
        {
        }
    }
}
