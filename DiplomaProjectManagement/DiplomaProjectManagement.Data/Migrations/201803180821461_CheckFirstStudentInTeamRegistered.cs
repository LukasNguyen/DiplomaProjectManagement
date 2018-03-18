namespace DiplomaProjectManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckFirstStudentInTeamRegistered : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiplomaProjectRegistrations", "IsFirstStudentInTeamRegistered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiplomaProjectRegistrations", "IsFirstStudentInTeamRegistered");
        }
    }
}
