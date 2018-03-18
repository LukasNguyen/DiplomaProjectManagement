using DiplomaProjectManagement.Common.CustomViewModel;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Models;
using System.Linq;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IDiplomaProjectRegistrationRepository : IRepository<DiplomaProjectRegistration>
    {
        bool IsCurrentStudentRegistered(int studentId, int registrationTimeId);

        DiplomaProjectDetailViewModel GetDiplomaProjectDetailByStudentId(int studentId);

        int FindDiplomaProject(int studentId, int registrationTimeId);

        string FindTeamName(int studentId, int registrationTimeId);
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

        public DiplomaProjectDetailViewModel GetDiplomaProjectDetailByStudentId(int studentId)
        {
            var diplomaProjectRegistrations =
                (from dpr in DbContext.DiplomaProjectRegistrations
                 join dp in DbContext.DiplomaProjects
                 on dpr.DiplomaProjectId equals dp.ID
                 join l in DbContext.Lecturers
                 on dp.LecturerId equals l.ID
                 where dpr.StudentId == studentId
                 select new
                 {
                     Name = dp.Name,
                     Description = dp.Description,
                     LecturerName = l.Name,
                     IntroducedGrades = dpr.IntroducedGrades,
                     ReviewedGrades = dpr.ReviewedGrades
                 })
                    .AsEnumerable();

            if (diplomaProjectRegistrations.Any())
            {
                return diplomaProjectRegistrations.Select(n => new DiplomaProjectDetailViewModel
                {
                    Name = n.Name,
                    Description = n.Description,
                    LecturerName = n.LecturerName,
                    IntroducedGrades = n.IntroducedGrades,
                    ReviewedGrades = n.ReviewedGrades
                })
                    .FirstOrDefault();
            }
            return null;
        }

        public int FindDiplomaProject(int studentId, int registrationTimeId)
        {
            return DbContext.DiplomaProjectRegistrations
                .Where(n => n.StudentId == studentId
                    && n.RegistrationTimeId == registrationTimeId)
                .Select(n => n.DiplomaProjectId)
                .SingleOrDefault();
        }

        public string FindTeamName(int studentId, int registrationTimeId)
        {
            return DbContext.DiplomaProjectRegistrations
                .Where(n => n.StudentId == studentId
                            && n.RegistrationTimeId == registrationTimeId)
                .Select(n => n.TeamName)
                .SingleOrDefault();
        }
    }
}