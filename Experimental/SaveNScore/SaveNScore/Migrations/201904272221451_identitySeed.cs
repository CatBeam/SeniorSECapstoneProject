namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class identitySeed : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [AddressStreet], [AddressCity], [AddressState], [AddressZipCode], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4b2911f3-d129-4275-911a-41a2b2de8f6d', N'Donald', N'Merrill', N'4914 N Newton Ave', N'Kansas City', N'Missouri', N'64134', N'test@test.com', 0, N'AJFB6BJJ1jKEpjs81Z71ubQasiHKC+6q316DczmbD7Yy6XnmqULLSVp5cCjPdmj0Lg==', N'36ed5b49-db44-44f0-96ba-8e848272844f', N'816-210-7639', 0, 0, NULL, 1, 0, N'test@test.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [AddressStreet], [AddressCity], [AddressState], [AddressZipCode], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'53f2c5a5-2ee2-4ddc-a344-460d848ff52b', N'Steve', N'Steverson', N'1111 N Way', N'Kansas City', N'Missouri', N'64134', N'steve@savenscore.com', 0, N'AK50oW/xpRF6oyBtjTJuBwL1DWAtlqxNZuDTzMlPcnhjKQbY9DE+OfMjWvYnXQrpIw==', N'796eefd7-0391-43d9-b578-c7797807d36e', N'816-111-1111', 0, 0, NULL, 1, 0, N'steve@savenscore.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [AddressStreet], [AddressCity], [AddressState], [AddressZipCode], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'df796b37-38ca-4d13-a6a9-a1f9a7947ba5', N'Ben', N'Rahman', N'1112 Somewhere', N'Kansas City', N'Missouri', N'64117', N'cat@cat.com', 0, N'AK16wdCfO7xqJR7BBw9QcjmPUZSi6n38mn7KwUUIW/6mU2SFIbytUgp3ZK/q13UmpA==', N'6d0df4ff-eaf8-413d-987f-463f4c81c80a', N'816-222-2222', 0, 0, NULL, 1, 0, N'cat@cat.com')
");
        }

        public override void Down()
        {
        }
    }
}