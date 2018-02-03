using System;
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

        Student DeleteStudentByModifyStatus(int id);

        IEnumerable<Student> GetAllStudents(string keyword = null);

        Student GetStudentById(int id);

        IEnumerable<Student> GetIntroducedStudentsByRegisterTimeId(int registerTimeId, int lecturerId);

        float? GetStudentReviewedGrades(int studentId, int diplomaProjectId);

        float? GetStudentIntroducedGrades(int studentId, int diplomaProjectId);

        void UpdateReviewedGradesStudent(int studentId, int diplomaProjectId, float score);

        void UpdateIntroducedGradesStudent(int studentId, int diplomaProjectId, float score);
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

        public Student DeleteStudentByModifyStatus(int id)
        {
            var student =  _studentRepository.GetSingleById(id);

            student.Status = false;

            return student;
        }

        public IEnumerable<Student> GetAllStudents(string keyword = null)
        {
            if (!String.IsNullOrWhiteSpace(keyword))
                return _studentRepository.GetMulti(
                    n => n.Name.Contains(keyword)
                    || n.ID.ToString() == keyword)
                    .Where(n => n.Status)
                    .ToList();

            return _studentRepository.GetAll().Where(n=>n.Status).ToList();
        }

        public Student GetStudentById(int id)
        {
            return _studentRepository.GetSingleById(id);
        }

        public IEnumerable<Student> GetIntroducedStudentsByRegisterTimeId(int registerTimeId, int lecturerId)
        {
            return _studentRepository.GetIntroducedStudentsByRegisterTimeId(registerTimeId, lecturerId);
        }

        public float? GetStudentReviewedGrades(int studentId, int diplomaProjectId)
        {
            return _studentRepository.GetStudentReviewedGrades(studentId, diplomaProjectId);
        }

        public float? GetStudentIntroducedGrades(int studentId, int diplomaProjectId)
        {
            return _studentRepository.GetStudentIntroducedGrades(studentId, diplomaProjectId);
        }

        public void UpdateReviewedGradesStudent(int studentId, int diplomaProjectId, float score)
        {
            _studentRepository.UpdateReviewedGradesStudent(studentId, diplomaProjectId, score);
        }

        public void UpdateIntroducedGradesStudent(int studentId, int diplomaProjectId, float score)
        {
            _studentRepository.UpdateIntroducedGradesStudent(studentId, diplomaProjectId, score);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}