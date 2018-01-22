using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Models;
using System.Linq;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        float? GetStudentScores(int studentId, int diplomaProjectId);
        void UpdateScoresStudent(int lecturerId,int studentId,float score);
    }

    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public float? GetStudentScores(int studentId, int diplomaProjectId)
        {
            var studentProjectRegistered = GetStudentProjectRegistered();

            return studentProjectRegistered?.Scores;

            Student GetStudentProjectRegistered()
            {
                return (from s in DbContext.Students
                    join dpr in DbContext.DiplomaProjectRegistrations
                    on s.ID equals dpr.StudentId
                    where s.ID == studentId && dpr.DiplomaProjectId == diplomaProjectId
                    select s).FirstOrDefault();
            }
        }

        public void UpdateScoresStudent(int lecturerId, int studentId, float score)
        {
            var studentProjectRegistered = GetSpecificStudentProjectRegistered();

            studentProjectRegistered.Scores = score;

            Student GetSpecificStudentProjectRegistered()
            {
                return (from s in DbContext.Students
                    join dpr in DbContext.DiplomaProjectRegistrations
                    on s.ID equals dpr.StudentId
                    join dp in DbContext.DiplomaProjects
                    on dpr.DiplomaProjectId equals dp.ID
                    join l in DbContext.Lecturers
                    on dp.LecturerId equals l.ID
                    where s.ID == studentId && l.ID == lecturerId
                    select s).FirstOrDefault();
            }
        }
    }
}