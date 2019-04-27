namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        AchievementNum = c.Int(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.UserID, t.AchievementNum });
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.CustomerAccounts",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        AccountNum = c.String(nullable: false, maxLength: 128),
                        AccountType = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => new { t.UserID, t.AccountNum });
            
            CreateTable(
                "dbo.CustomerTransactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        AccountNum = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        TransactionType = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.TransactionID);
            
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        GoalID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        AccountNum = c.String(),
                        GoalType = c.Int(nullable: false),
                        GoalPeriod = c.Int(nullable: false),
                        StartValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LimitValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Description = c.String(nullable: false, maxLength: 500),
                        Completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GoalID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AddressStreet = c.String(),
                        AddressCity = c.String(),
                        AddressState = c.String(),
                        AddressZipCode = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Goals");
            DropTable("dbo.CustomerTransactions");
            DropTable("dbo.CustomerAccounts");
            DropTable("dbo.Customers");
            DropTable("dbo.Achievements");
        }
    }
}
