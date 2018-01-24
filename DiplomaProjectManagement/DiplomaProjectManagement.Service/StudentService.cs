using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace DiplomaProjectManagement.Service
{
    public interface IStudentService
    {
        Student AddStudent(Student student);

        void UpdateStudent(Student student);

        Student DeleteStudent(int id);

        IEnumerable<Student> GetAllStudents();

        float? GetStudentScores(int studentId, int diplomaProjectId);

        IEnumerable<Student> GetIntroducedStudentsByRegisterTimeId(int registerTimeId, int lecturerId);

        void UpdateScoresStudent(int lecturerId, int studentId, float score);

        void Save();
    }

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public Student AddStudent(Student student)
        {
            return _studentRepository.Add(student);
        }

        public void UpdateStudent(Student student)
        {
            _studentRepository.Update(student);
        }

        public Student DeleteStudent(int id)
        {
            return _studentRepository.Delete(id);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentRepository.GetAll().ToList();
        }

        public float? GetStudentScores(int studentId, int diplomaProjectId)
        {
            return _studentRepository.GetStudentScores(studentId, diplomaProjectId);
        }

        public IEnumerable<Student> GetIntroducedStudentsByRegisterTimeId(int registerTimeId, int lecturerId)
        {
            return _studentRepository.GetIntroducedStudentsByRegisterTimeId(registerTimeId, lecturerId);
        }

        public void UpdateScoresStudent(int lecturerId, int studentId, float score)
        {
            _studentRepository.UpdateScoresStudent(lecturerId, studentId, score);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}