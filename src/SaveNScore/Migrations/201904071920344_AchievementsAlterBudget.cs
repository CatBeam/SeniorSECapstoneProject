namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AchievementsAlterBudget : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        AchievementID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Description = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AchievementID);
            
            AddColumn("dbo.Budgets", "BudgetDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Budgets", "BudgetDescription");
            DropTable("dbo.Achievements");
        }
    }
}
