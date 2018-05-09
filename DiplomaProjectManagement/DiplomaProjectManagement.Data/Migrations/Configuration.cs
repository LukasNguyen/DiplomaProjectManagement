using DiplomaProjectManagement.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Model.Enums;

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
                new Lecturer { Name = "Lukas Nguyen", Address = "Ho Chi Minh City", Email  = "lukas.nguyen@iuh.edu.vn", Phone = "0949209492", Status = true , FacilityId = 1 },
                new Lecturer { Name = "Dakas Nguyen", Address = "Ho Chi Minh City", Email  = "dakas.nguyen@iuh.edu.vn", Phone = "0949209491", Status = true, FacilityId = 1 }
            });
            context.SaveChanges();
        }

        private void CreateSampleRegistrationTimesData(DiplomaProjectDbContext context)
        {
            if (context.RegistrationTimes.Any())
                return;

            context.RegistrationTimes.AddRange(new List<RegistrationTime>()
            {
                new RegistrationTime { Name = "Đợt đăng ký kì 1 2018-2019", RegisteredDate = new DateTime(2018,1,1), TeacherAssignGradesDate = new DateTime(2018,4,1), ClosedDate = new DateTime(2018,6,1) },
            });
            context.SaveChanges();
        }

        private void CreateSampleDiplomaProjectData(DiplomaProjectDbContext context)
        {
            if (context.DiplomaProjects.Any())
                return;

            context.DiplomaProjects.AddRange(new List<DiplomaProject>()
            {
                new DiplomaProject { Name = "Xây dựng website quản lý đồ án", Description = "Nghiên cứu xây dựng website quản lý đồ án bằng ASP.NET", LecturerId = 1, Status = true, IsDisplayed = true},
                new DiplomaProject { Name = "Xây dựng website quản lý học phần", Description = "Nghiên cứu xây dựng website quản lý học phần bằng NodeJS", LecturerId = 2, Status = true, IsDisplayed = true },
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
                new DiplomaProjectRegistration { DiplomaProjectId = 1, RegistrationTimeId = 1, StudentId = 1 },
            });
            context.SaveChanges();
        }

        private void CreateUserExample(DiplomaProjectDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var user = new ApplicationUser()
            {
                UserName = "admin@iuh.edu.vn",
                Email = "admin@iuh.edu.vn"
            };

            if (manager.Users.Count(n => n.UserName == user.UserName) == 0)
            {
                manager.Create(user, "123456789");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole() { Name = RoleConstants.Admin });
                    roleManager.Create(new IdentityRole() { Name = RoleConstants.Lecturer });
                    roleManager.Create(new IdentityRole() { Name = RoleConstants.Student });

                    var adminUser = manager.FindByEmail(user.Email);

                    manager.AddToRoles(adminUser.Id, RoleConstants.Admin);
                }

                AddNewUser(manager, "dat.nguyen@gmail.com", RoleConstants.Student);
                AddNewUser(manager, "lukas.nguyen@gmail.com", RoleConstants.Student);
                AddNewUser(manager, "dakas.nguyen@gmail.com", RoleConstants.Student);

                AddNewUser(manager, "dat.nguyen@iuh.edu.vn", RoleConstants.Lecturer);
                AddNewUser(manager, "lukas.nguyen@iuh.edu.vn", RoleConstants.Lecturer);
                AddNewUser(manager, "dakas.nguyen@iuh.edu.vn", RoleConstants.Lecturer);
            }


            context.SaveChanges();
        }

        private static void AddNewUser(UserManager<ApplicationUser> manager,string email,string role)
        {
            var user = new ApplicationUser()
            {
                UserName = email,
                Email = email
            };

            if (manager.Users.Count(n => n.Email == email) == 0)
            {
                manager.Create(user, "123456789");

                manager.AddToRole(user.Id, role);
            }
        }
    }
}