namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BenMig1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Achievements");
            AddColumn("dbo.Achievements", "AchType", c => c.Int(nullable: false));
            AddColumn("dbo.Achievements", "CountToUnlock", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Achievements", new[] { "UserID", "AchType" });
            DropColumn("dbo.Achievements", "AchievementNum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Achievements", "AchievementNum", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Achievements");
            DropColumn("dbo.Achievements", "CountToUnlock");
            DropColumn("dbo.Achievements", "AchType");
            AddPrimaryKey("dbo.Achievements", new[] { "UserID", "AchievementNum" });
        }
    }
}
