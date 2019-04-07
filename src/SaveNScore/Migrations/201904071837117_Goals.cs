namespace SaveNScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Goals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        GoalID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        GoalDescription = c.String(),
                        Type = c.Int(nullable: false),
                        StartValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GoalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GoalID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Goals");
        }
    }
}
