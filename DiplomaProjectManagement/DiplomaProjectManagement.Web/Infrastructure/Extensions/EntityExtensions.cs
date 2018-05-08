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
            student.CreatedDate = DateTime.Now;
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
            lecturer.CreatedDate = DateTime.Now;
        }
    }
}