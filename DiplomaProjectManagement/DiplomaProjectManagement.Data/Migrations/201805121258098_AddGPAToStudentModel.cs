namespace DiplomaProjectManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGPAToStudentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "GPA", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "GPA");
        }
    }
}
