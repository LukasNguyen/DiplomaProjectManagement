namespace DiplomaProjectManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ChangePropertyToTeacherAssignGrades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegistrationTimes", "TeacherAssignGradesDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.RegistrationTimes", "ClosedRegisteredDate");
            DropColumn("dbo.RegistrationTimes", "ClosedDate");
            AddColumn("dbo.RegistrationTimes", "ClosedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.RegistrationTimes", "RegistrationStatus");
            AddColumn("dbo.RegistrationTimes", "RegistrationStatus", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            AddColumn("dbo.RegistrationTimes", "ClosedRegisteredDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.RegistrationTimes", "TeacherAssignGradesDate");
        }
    }
}
