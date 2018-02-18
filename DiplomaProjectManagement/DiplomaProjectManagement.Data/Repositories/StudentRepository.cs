using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        DiplomaProjectRegistration GetStudentRegistrationdByStudentIdAndDiplomaId(
            int studentId,
            int diplomaProjectId);

        float? GetStudentReviewedGrades(int studentId, int diplomaProjectId);

        float? GetStudentIntroducedGrades(int studentId, int diplomaProjectId);

        void UpdateReviewedGradesStudent(int studentId, int diplomaProjectId, float score);

        void UpdateIntroducedGradesStudent(int studentId, int diplomaProjectId, float score);

        IEnumerable<Student> GetIntroducedStudentsByRegisterTimeId(int registerTimeId, int lecturerId);
    }

    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public float? GetStudentReviewedGrades(int studentId, int diplomaProjectId)
        {
            var studentRegistration = GetStudentRegistrationdByStudentIdAndDiplomaId(studentId, diplomaProjectId);

            return studentRegistration?.ReviewedGrades;
        }

        public float? GetStudentIntroducedGrades(int studentId, int diplomaProjectId)
        {
            var studentRegistration = GetStudentRegistrationdByStudentIdAndDiplomaId(studentId, diplomaProjectId);

            return studentRegistration?.IntroducedGrades;
        }

        public DiplomaProjectRegistration GetStudentRegistrationdByStudentIdAndDiplomaId(int studentId, int diplomaProjectId)
        {
            return (from dpr in DbContext.DiplomaProjectRegistrations
                    join s in DbContext.Students
                    on dpr.StudentId equals s.ID
                    where s.ID == studentId && dpr.DiplomaProjectId == diplomaProjectId
                    select dpr).FirstOrDefault();
        }

        public void UpdateIntroducedGradesStudent(int studentId, int diplomaProjectId, float score)
        {
            var studentRegistration = GetStudentRegistrationdByStudentIdAndDiplomaId(studentId, diplomaProjectId);

            if (studentRegistration != null)
                studentRegistration.IntroducedGrades = score;
        }

        public void UpdateReviewedGradesStudent(int studentId, int diplomaProjectId, float score)
        {
            var studentRegistration = GetStudentRegistrationdByStudentIdAndDiplomaId(studentId, diplomaProjectId);

            if (studentRegistration != null)
                studentRegistration.ReviewedGrades = score;
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