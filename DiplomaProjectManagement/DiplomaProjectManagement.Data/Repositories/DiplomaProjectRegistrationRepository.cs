using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Models;
using System.Linq;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IDiplomaProjectRegistrationRepository : IRepository<DiplomaProjectRegistration>
    {
        DiplomaProjectRegistration IsExistsStudentRegistered(int id);
    }

    public class DiplomaProjectRegistrationRepository : RepositoryBase<DiplomaProjectRegistration>, IDiplomaProjectRegistrationRepository
    {
        public DiplomaProjectRegistrationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public DiplomaProjectRegistration IsExistsStudentRegistered(int id)
        {
            DiplomaProjectRegistration studentRegistered = GetStudentRegistered();

            return studentRegistered ?? null;

            DiplomaProjectRegistration GetStudentRegistered()
            {
                return (from dpr in DbContext.DiplomaProjectRegistrations
                    join s in DbContext.Students
                    on dpr.StudentId equals s.ID
                    where s.ID == id
                    select dpr).FirstOrDefault();
            }
        }
    }
}