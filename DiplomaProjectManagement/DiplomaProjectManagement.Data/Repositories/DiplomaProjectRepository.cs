using DiplomaProjectManagement.Common.CustomViewModel;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Enums;
using DiplomaProjectManagement.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IDiplomaProjectRepository : IRepository<DiplomaProject>
    {
        IQueryable<DiplomaProject> GetDiplomaProjectsByLecturerId(int lectureId);

        DiplomaProject GetDiplomaProjectByStudentId(int studentId);

        IEnumerable<DiplomaProjectRemainingSlotViewModel> GetDiplomaProjectsRemainingSlot();
    }

    public class DiplomaProjectRepository : RepositoryBase<DiplomaProject>, IDiplomaProjectRepository
    {
        public DiplomaProjectRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IQueryable<DiplomaProject> GetDiplomaProjectsByLecturerId(int lectureId)
        {
            return (from dp in DbContext.DiplomaProjects
                    where dp.Status && dp.LecturerId == lectureId
                    select dp);
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
                    where s.ID == studentId
                    select dp).FirstOrDefault();
        }

        public IEnumerable<DiplomaProjectRemainingSlotViewModel> GetDiplomaProjectsRemainingSlot()
        {
            return (from dpr in DbContext.DiplomaProjectRegistrations
                     join s in DbContext.Students
                 on dpr.StudentId equals s.ID
                 join rt in DbContext.RegistrationTimes
                 on dpr.RegistrationTimeId equals rt.ID
                 join dp in DbContext.DiplomaProjects
                 on dpr.DiplomaProjectId equals dp.ID
                 join l in DbContext.Lecturers
                 on dp.LecturerId equals l.ID
                 where rt.RegistrationStatus == RegistrationStatus.Opening
                    && dpr.TeamName == null
                 select new
                 {
                     DiplomaProjectName = dp.Name,
                     LecturerName = l.Name,
                     StudentName = s.Name,
                     StudentEmail = s.Email,
                     StudentPhone = s.Phone
                 })
                    .AsEnumerable()
                    .Select(n => new DiplomaProjectRemainingSlotViewModel
                    {
                        DiplomaProjectName = n.DiplomaProjectName,
                        LecturerName = n.LecturerName,
                        StudentName = n.StudentName,
                        StudentEmail = n.StudentEmail,
                        StudentPhone = n.StudentPhone
                    });
        }
    }
}