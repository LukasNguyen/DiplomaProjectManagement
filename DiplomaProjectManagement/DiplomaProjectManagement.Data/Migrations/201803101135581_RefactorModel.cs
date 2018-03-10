namespace DiplomaProjectManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiplomaProjects", "IsDisplayed", c => c.Boolean(nullable: false));
            DropColumn("dbo.DiplomaProjects", "NumberOfStudentsRegistered");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DiplomaProjects", "NumberOfStudentsRegistered", c => c.Int(nullable: false));
            DropColumn("dbo.DiplomaProjects", "IsDisplayed");
        }
    }
}
