using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace DiplomaProjectManagement.Service
{
    public interface ILecturerService
    {
        Lecturer AddLecturer(Lecturer lecturer);

        void UpdateLecturer(Lecturer lecturer);

        Lecturer DeleteLecturer(int id);

        IEnumerable<Lecturer> GetAlLecturers();

        void Save();
    }

    public class LecturerService : ILecturerService
    {
        private readonly ILecturerRepository _lecturerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LecturerService(ILecturerRepository lecturerRepository, IUnitOfWork unitOfWork)
        {
            _lecturerRepository = lecturerRepository;
            _unitOfWork = unitOfWork;
        }

        public Lecturer AddLecturer(Lecturer lecturer)
        {
            return _lecturerRepository.Add(lecturer);
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            _lecturerRepository.Update(lecturer);
        }

        public Lecturer DeleteLecturer(int id)
        {
            return _lecturerRepository.Delete(id);
        }

        public IEnumerable<Lecturer> GetAlLecturers()
        {
            return _lecturerRepository.GetAll().ToList();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}