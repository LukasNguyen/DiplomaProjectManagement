namespace DiplomaProjectManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserLoginAndGrantPermission : DbMigration
    {
        public override void Up()
        {
            Sql(@"USE [DiplomaProjectManagement]
CREATE LOGIN[lukas_nguyen] WITH PASSWORD = N'Dat123456789', DEFAULT_DATABASE = [DiplomaProjectManagement], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF
CREATE USER[lukas_nguyen] FOR LOGIN[lukas_nguyen]
ALTER ROLE[db_owner] ADD MEMBER[lukas_nguyen]
");
        }
        
        public override void Down()
        {
        }
    }
}
