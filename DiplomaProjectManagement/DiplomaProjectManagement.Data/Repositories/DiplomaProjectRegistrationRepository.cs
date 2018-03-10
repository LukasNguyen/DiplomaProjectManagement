using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Models;
using System.Linq;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IDiplomaProjectRegistrationRepository : IRepository<DiplomaProjectRegistration>
    {
        bool IsCurrentStudentRegistered(int studentId, int registrationTimeId);
    }

    public class DiplomaProjectRegistrationRepository : RepositoryBase<DiplomaProjectRegistration>, IDiplomaProjectRegistrationRepository
    {
        public DiplomaProjectRegistrationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public bool IsCurrentStudentRegistered(int studentId, int registrationTimeId)
        {
            return DbContext.DiplomaProjectRegistrations
                .Any(n => n.StudentId == studentId
                    && n.RegistrationTimeId == registrationTimeId);
        }
    }
}