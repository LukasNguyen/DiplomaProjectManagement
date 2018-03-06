namespace DiplomaProjectManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateBusinessLogicFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiplomaProjectRegistrations", "IntroducedGrades", c => c.Single());
            AddColumn("dbo.DiplomaProjectRegistrations", "ReviewedGrades", c => c.Single());
            AddColumn("dbo.DiplomaProjectRegistrations", "RegistrationStatus", c => c.Int(nullable: false));
            AddColumn("dbo.DiplomaProjects", "NumberOfStudentsRegistered", c => c.Int(nullable: false));
            AlterColumn("dbo.RegistrationTimes", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.DiplomaProjectRegistrations", "limitedStudentRegister");
            DropColumn("dbo.DiplomaProjectRegistrations", "isOpened");
            DropColumn("dbo.Students", "Scores");
        }

        public override void Down()
        {
            AddColumn("dbo.Students", "Scores", c => c.Single());
            AddColumn("dbo.DiplomaProjectRegistrations", "isOpened", c => c.Boolean(nullable: false));
            AddColumn("dbo.DiplomaProjectRegistrations", "limitedStudentRegister", c => c.Int(nullable: false));
            AlterColumn("dbo.RegistrationTimes", "Name", c => c.String(maxLength: 50));
            DropColumn("dbo.DiplomaProjects", "NumberOfStudentsRegistered");
            DropColumn("dbo.DiplomaProjectRegistrations", "RegistrationStatus");
            DropColumn("dbo.DiplomaProjectRegistrations", "ReviewedGrades");
            DropColumn("dbo.DiplomaProjectRegistrations", "IntroducedGrades");
        }
    }
}
