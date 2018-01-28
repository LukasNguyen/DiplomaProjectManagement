using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IDiplomaProjectRepository : IRepository<DiplomaProject>
    {
        IEnumerable<DiplomaProject> GetDiplomaProjectsActiveInRegisterTime();

        DiplomaProject GetDiplomaProjectByStudentId(int studentId);
    }

    public class DiplomaProjectRepository : RepositoryBase<DiplomaProject>, IDiplomaProjectRepository
    {
        public DiplomaProjectRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<DiplomaProject> GetDiplomaProjectsActiveInRegisterTime()
        {
            return (from dp in DbContext.DiplomaProjects
                    join dpr in DbContext.DiplomaProjectRegistrations
                    on dp.ID equals dpr.DiplomaProjectId
                    join rt in DbContext.RegistrationTimes
                    on dpr.RegistrationTimeId equals rt.ID
                    where rt.Status && dp.Status
                    select dp).ToList();
        }

        public DiplomaProject GetDiplomaProjectByStudentId(int studentId)
        {
            return (from dp in DbContext.DiplomaProjects
                    join dpr in DbContext.DiplomaProjectRegistrations
                    on dp.ID equals dpr.DiplomaProjectId
                    join s in DbContext.Students
                    on dpr.StudentId equals s.ID
                    join rt in DbContext.RegistrationTimes
                    on dpr.RegistrationTimeId equals rt.ID
                    where s.ID == studentId && rt.Status
                    select dp).FirstOrDefault();
        }
    }
}