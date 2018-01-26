namespace DiplomaProjectManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequireSomeVitalFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegistrationTimes", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Lecturers", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Students", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ApplicationUsers", "Email", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUsers", "Email", c => c.String());
            AlterColumn("dbo.Students", "Email", c => c.String(maxLength: 250));
            AlterColumn("dbo.Lecturers", "Email", c => c.String(maxLength: 250));
            DropColumn("dbo.RegistrationTimes", "Status");
        }
    }
}
