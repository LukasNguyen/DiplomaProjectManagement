namespace DiplomaProjectManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ModifyLogicRegisterTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegistrationTimes", "ClosedRegisteredDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RegistrationTimes", "ClosedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RegistrationTimes", "RegistrationStatus", c => c.Int(nullable: false));
            DropColumn("dbo.DiplomaProjectRegistrations", "RegistrationStatus");
            DropColumn("dbo.RegistrationTimes", "FinishedDate");
            DropColumn("dbo.RegistrationTimes", "Status");
        }

        public override void Down()
        {
            AddColumn("dbo.RegistrationTimes", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.RegistrationTimes", "FinishedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DiplomaProjectRegistrations", "RegistrationStatus", c => c.Int(nullable: false));
            DropColumn("dbo.RegistrationTimes", "RegistrationStatus");
            DropColumn("dbo.RegistrationTimes", "ClosedDate");
            DropColumn("dbo.RegistrationTimes", "ClosedRegisteredDate");
        }
    }
}
