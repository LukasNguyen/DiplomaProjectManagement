using System;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Enums;
using DiplomaProjectManagement.Model.Models;
using System.Linq;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IRegistrationTimeRepository : IRepository<RegistrationTime>
    {
        bool CheckExistingRegistrationTimeOpening();

        void UpdateRegistrationTimeStatusToTeacherAssignGrades();

        void UpdateRegistrationTimeStatusToCloseRegistrationTime();
    }

    public class RegistrationTimeRepository : RepositoryBase<RegistrationTime>, IRegistrationTimeRepository
    {
        public RegistrationTimeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public bool CheckExistingRegistrationTimeOpening()
        {
            return DbContext.RegistrationTimes.Any(n => n.RegistrationStatus == RegistrationStatus.Opening);
        }

        public void UpdateRegistrationTimeStatusToTeacherAssignGrades()
        {
            var updatedRegistrationTimes =
                DbContext.RegistrationTimes.Where(n => n.TeacherAssignGradesDate <= DateTime.Now).ToList();

            updatedRegistrationTimes.ForEach(n => n.RegistrationStatus = RegistrationStatus.ClosedRegistrationTime);
        }

        public void UpdateRegistrationTimeStatusToCloseRegistrationTime()
        {
            var updatedRegistrationTimes =
                DbContext.RegistrationTimes.Where(n => n.ClosedDate <= DateTime.Now).ToList();

            updatedRegistrationTimes.ForEach(n => n.RegistrationStatus = RegistrationStatus.ClosedAssignGradesTime);
        }
    }
}