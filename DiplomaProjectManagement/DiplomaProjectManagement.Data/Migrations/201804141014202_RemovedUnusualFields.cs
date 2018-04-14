namespace DiplomaProjectManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedUnusualFields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DiplomaProjects", "CreatedBy");
            DropColumn("dbo.DiplomaProjects", "UpdatedDate");
            DropColumn("dbo.DiplomaProjects", "UpdatedBy");
            DropColumn("dbo.Lecturers", "CreatedBy");
            DropColumn("dbo.Lecturers", "UpdatedDate");
            DropColumn("dbo.Lecturers", "UpdatedBy");
            DropColumn("dbo.Facilities", "CreatedBy");
            DropColumn("dbo.Facilities", "UpdatedDate");
            DropColumn("dbo.Facilities", "UpdatedBy");
            DropColumn("dbo.Students", "CreatedBy");
            DropColumn("dbo.Students", "UpdatedDate");
            DropColumn("dbo.Students", "UpdatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Students", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Students", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Facilities", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Facilities", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Facilities", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Lecturers", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Lecturers", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Lecturers", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.DiplomaProjects", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.DiplomaProjects", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.DiplomaProjects", "CreatedBy", c => c.String(maxLength: 256));
        }
    }
}
