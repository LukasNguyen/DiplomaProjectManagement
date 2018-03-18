namespace DiplomaProjectManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeamNameProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiplomaProjectRegistrations", "TeamName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiplomaProjectRegistrations", "TeamName");
        }
    }
}
