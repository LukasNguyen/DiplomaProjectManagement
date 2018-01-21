using DiplomaProjectManagement.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProjectManagement.Data
{
    public class DiplomaProjectDbContext : IdentityDbContext<ApplicationUser>
    {
        public DiplomaProjectDbContext() : base("DiplomaProjectConnection")
        {
            Configuration.LazyLoadingEnabled = false; // load bảng cha không tự đông load thêm bảng con
        }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        
        public DbSet<DiplomaProject> DiplomaProjects { get; set; }
        
        public DbSet<RegistrationTime> RegistrationTimes { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }
        
        public DbSet<Student> Students { get; set; }

        public DbSet<DiplomaProjectRegistration> DiplomaProjectRegistrations { get; set; }

        public static DiplomaProjectDbContext Create()
        {
            return new DiplomaProjectDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Set khóa chính cho các bảng
            modelBuilder.Entity<IdentityUserRole>().HasKey(n => new { n.UserId, n.RoleId }).ToTable("ApplicationUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(n => n.UserId).ToTable("ApplicationUserLogins");
            modelBuilder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            modelBuilder.Entity<IdentityUserClaim>().HasKey(n => n.UserId).ToTable("ApplicationUserClaims");
        }
    }
}
