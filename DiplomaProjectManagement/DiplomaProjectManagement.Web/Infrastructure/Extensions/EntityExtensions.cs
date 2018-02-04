using System;
using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Web.Models;

namespace DiplomaProjectManagement.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void Update(this Student student, StudentLoginViewModel studentLoginViewModel)
        {
            student.ID = studentLoginViewModel.ID;
            student.Address = studentLoginViewModel.Address;
            student.Email = studentLoginViewModel.Email;
            student.Name = studentLoginViewModel.Name;
            student.Phone = studentLoginViewModel.Phone;
            student.Status = studentLoginViewModel.Status;
        }

        public static void CreatedBy(this Student student, string triggeredBy)
        {
            student.CreatedDate = DateTime.Now;
            student.CreatedBy = triggeredBy;
        }

        public static void UpdatedBy(this Student student, string triggeredBy)
        {
            student.UpdatedDate = DateTime.Now;
            student.UpdatedBy = triggeredBy;
        }

        public static void Update(this ApplicationUser applicationUser, StudentLoginViewModel studentLoginViewModel)
        {
            applicationUser.Id = studentLoginViewModel.ApplicationUserId;
            applicationUser.Email = studentLoginViewModel.Email;
            applicationUser.UserName = studentLoginViewModel.Email;
        }
    }
}