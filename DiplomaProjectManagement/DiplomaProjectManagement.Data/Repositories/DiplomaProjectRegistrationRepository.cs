using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Models;
using System.Linq;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IDiplomaProjectRegistrationRepository : IRepository<DiplomaProjectRegistration>
    {
        bool IsExistsStudentRegistered(int studentId, int diplomaProjectId);
    }

    public class DiplomaProjectRegistrationRepository : RepositoryBase<DiplomaProjectRegistration>, IDiplomaProjectRegistrationRepository
    {
        public DiplomaProjectRegistrationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public bool IsExistsStudentRegistered(int studentId, int diplomaProjectId)
        {
                return (from dpr in DbContext.DiplomaProjectRegistrations
                    join s in DbContext.Students
                    on dpr.StudentId equals s.ID
                    join dp in DbContext.DiplomaProjects
                    on dpr.DiplomaProjectId equals dp.ID
                    where s.ID == studentId && dp.ID == diplomaProjectId
                        select dpr).Any();
        }
    }
}