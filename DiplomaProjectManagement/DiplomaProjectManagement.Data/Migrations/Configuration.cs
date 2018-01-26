using DiplomaProjectManagement.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DiplomaProjectManagement.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaProjectManagement.Data.DiplomaProjectDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DiplomaProjectDbContext context)
        {
            CreateSampleStudentsData(context);
            CreateSampleFacilitiesData(context);
            CreateSampleLecturersData(context);
            CreateSampleRegistrationTimesData(context);
            CreateSampleDiplomaProjectData(context);
            CreateSampleDiplomaProjectRegistrationData(context);
            CreateUserExample(context);
        }

        private void CreateSampleStudentsData(DiplomaProjectDbContext context)
        {
            if (context.Students.Any())
                return;

            context.Students.AddRange(new List<Student>()
            {
                new Student { Name = "Dat Nguyen", Address = "Ho Chi Minh City", Email  = "dat.nguyen@gmail.com", Phone = "0949209493", Status = true },
                new Student { Name = "Lukas Nguyen", Address = "Ho Chi Minh City", Email  = "lukas.nguyen@gmail.com", Phone = "0949209492", Status = true },
                new Student { Name = "Dakas Nguyen", Address = "Ho Chi Minh City", Email  = "dakas.nguyen@gmail.com", Phone = "0949209491", Status = true }
            });
            context.SaveChanges();
        }

        private void CreateSampleFacilitiesData(DiplomaProjectDbContext context)
        {
            if (context.Facilities.Any())
                return;

            context.Facilities.AddRange(new List<Facility>()
            {
                new Facility { Name = "Công nghệ thông tin", LocationBuilding = "H", UrlWebsite = "fit.iuh.edu.vn", Status = true}
            });
            context.SaveChanges();
        }

        private void CreateSampleLecturersData(DiplomaProjectDbContext context)
        {
            if (context.Lecturers.Any())
                return;

            context.Lecturers.AddRange(new List<Lecturer>()
            {
                new Lecturer { Name = "Dat Nguyen", Address = "Ho Chi Minh City", Email  = "dat.nguyen@iuh.edu.vn", Phone = "0949209493", Status = true, FacilityId = 1 },
                new Lecturer { Name = "Lukas Nguyen", Address = "Ho Chi Minh City", Email  = "lukas.nguyen@edu.vn", Phone = "0949209492", Status = true , FacilityId = 1 },
                new Lecturer { Name = "Dakas Nguyen", Address = "Ho Chi Minh City", Email  = "dakas.nguyen@edu.vn", Phone = "0949209491", Status = true, FacilityId = 1 }
            });
            context.SaveChanges();
        }

        private void CreateSampleRegistrationTimesData(DiplomaProjectDbContext context)
        {
            if (context.RegistrationTimes.Any())
                return;

            context.RegistrationTimes.AddRange(new List<RegistrationTime>()
            {
                new RegistrationTime { Name = "Đợt đăng ký kì 1 2017-2018", RegisteredDate = new DateTime(2017,1,1), FinishedDate = new DateTime(2017,4,1), Status = true},
                new RegistrationTime { Name = "Đợt đăng ký kì 2 2017-2018", RegisteredDate = new DateTime(2017,6,1), FinishedDate = new DateTime(2017,12,1), Status = true},
            });
            context.SaveChanges();
        }

        private void CreateSampleDiplomaProjectData(DiplomaProjectDbContext context)
        {
            if (context.DiplomaProjects.Any())
                return;

            context.DiplomaProjects.AddRange(new List<DiplomaProject>()
            {
                new DiplomaProject { Name = "Xây dựng website quản lý đồ án", Description = "Nghiên cứu xây dựng website quản lý đồ án bằng ASP.NET", LecturerId = 1, Status = true },
                new DiplomaProject { Name = "Xây dựng website quản lý học phần", Description = "Nghiên cứu xây dựng website quản lý học phần bằng NodeJS", LecturerId = 2, Status = true },
                new DiplomaProject { Name = "Xây dựng phần mềm quản lý nhà hàng", Description = "Nghiên cứu xây dựng phần mềm quản lý nhà hàng bằng Windows Form Application", LecturerId = 1, Status = true }
            });
            context.SaveChanges();
        }

        private void CreateSampleDiplomaProjectRegistrationData(DiplomaProjectDbContext context)
        {
            if (context.DiplomaProjectRegistrations.Any())
                return;

            context.DiplomaProjectRegistrations.AddRange(new List<DiplomaProjectRegistration>()
            {
                new DiplomaProjectRegistration { DiplomaProjectId = 1, RegistrationTimeId = 1, StudentId = 1, isOpened = true, limitedStudentRegister = 2},
            });
            context.SaveChanges();
        }

        private void CreateUserExample(DiplomaProjectDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var user = new ApplicationUser()
            {
                UserName = "Administrator",
                Email = "dat.nguyenthaithanh@hotmail.com"
            };

            if (manager.Users.Count(n => n.UserName == "Administrator") == 0)
            {
                manager.Create(user, "123456789");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole() { Name = "Admin" });
                    roleManager.Create(new IdentityRole() { Name = "Lecturer" });
                    roleManager.Create(new IdentityRole() { Name = "Student" });

                    var adminUser = manager.FindByEmail("dat.nguyenthaithanh@hotmail.com");

                    manager.AddToRoles(adminUser.Id, "Admin");
                }
            }

            context.SaveChanges();
        }
    }
}