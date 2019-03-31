namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdmin : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [AddressStreet], [AddressCity], [AddressState], [AddressZipCode]) VALUES (N'acebac15-0d10-42d5-9d19-ae397708a48a', N'admin@savenscore.com', 0, N'AD/Lt4lC1XpY7WETMNFOXpotF4SLiJDpyCc9Eqfkz3K601BkOavhQVM288cjGSsE2g==', N'd240f38c-359a-48ac-b94f-bbbfb30b9b54', NULL, 0, 0, NULL, 1, 0, N'admin@savenscore.com', N'Neil', N'Pirch', N'2501 NW Acorn Drive', N'Blue Springs', N'MO', N'64014')
                INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'5908ad8b-b839-4aa0-b28e-55db70477369', N'AppAdmin')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'acebac15-0d10-42d5-9d19-ae397708a48a', N'5908ad8b-b839-4aa0-b28e-55db70477369')
                ");

        }

    public override void Down()
        {
        }
    }
}
