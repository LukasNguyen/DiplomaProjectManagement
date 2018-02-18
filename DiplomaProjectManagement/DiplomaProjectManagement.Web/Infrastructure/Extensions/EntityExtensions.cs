using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Web.Models;
using System;

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

        public static void Update(this Lecturer lecturer, LecturerLoginViewModel lecturerViewModel)
        {
            lecturer.ID = lecturerViewModel.ID;
            lecturer.FacilityId = lecturerViewModel.FacilityId;
            lecturer.Address = lecturerViewModel.Address;
            lecturer.Email = lecturerViewModel.Email;
            lecturer.Name = lecturerViewModel.Name;
            lecturer.Phone = lecturerViewModel.Phone;
            lecturer.Status = lecturerViewModel.Status;
        }

        public static void CreatedBy(this Lecturer lecturer, string triggeredBy)
        {
            lecturer.CreatedDate = DateTime.Now;
            lecturer.CreatedBy = triggeredBy;
        }

        public static void UpdatedBy(this Lecturer lecturer, string triggeredBy)
        {
            lecturer.UpdatedDate = DateTime.Now;
            lecturer.UpdatedBy = triggeredBy;
        }
    }
}