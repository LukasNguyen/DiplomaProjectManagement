using DiplomaProjectManagement.Model.Models;
using DiplomaProjectManagement.Web.Models;

namespace DiplomaProjectManagement.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void Update(this Student student, StudentLoginViewModel studentLoginViewModel, string createdBy)
        {
            student.ID = studentLoginViewModel.ID;
            student.Address = studentLoginViewModel.Address;
            student.Email = studentLoginViewModel.Email;
            student.Name = studentLoginViewModel.Name;
            student.Phone = studentLoginViewModel.Phone;
            student.CreatedDate = studentLoginViewModel.CreatedDate;
            student.Status = studentLoginViewModel.Status;
            student.CreatedBy = createdBy;
        }

        public static void Update(this ApplicationUser applicationUser, StudentLoginViewModel studentLoginViewModel)
        {
            applicationUser.Id = studentLoginViewModel.ApplicationUserId;
            applicationUser.Email = studentLoginViewModel.Email;
            applicationUser.UserName = studentLoginViewModel.Email;
        }
    }
}