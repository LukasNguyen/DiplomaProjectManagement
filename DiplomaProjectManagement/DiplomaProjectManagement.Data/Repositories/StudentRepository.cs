using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        //float? GetStudentScores(int studentId, int diplomaProjectId);

        // UpdateScoresStudent(int lecturerId, int studentId, float score);

        IEnumerable<Student> GetIntroducedStudentsByRegisterTimeId(int registerTimeId, int lecturerId);
    }

    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Student> GetIntroducedStudentsByRegisterTimeId(int registerTimeId, int lecturerId)
        {
            return GetIntroducedStudentsByRegisterTimeId();

            List<Student> GetIntroducedStudentsByRegisterTimeId()
            {
                return (from s in DbContext.Students
                        join dpr in DbContext.DiplomaProjectRegistrations
                        on s.ID equals dpr.StudentId
                        join dp in DbContext.DiplomaProjects
                        on dpr.DiplomaProjectId equals dp.ID
                        join l in DbContext.Lecturers
                        on dp.LecturerId equals l.ID
                        where l.ID == lecturerId && dpr.RegistrationTimeId == registerTimeId
                        select s).ToList();
            }
        }
    }
}