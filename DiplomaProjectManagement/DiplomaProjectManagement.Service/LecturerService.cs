using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiplomaProjectManagement.Service
{
    public interface ILecturerService
    {
        Lecturer AddLecturer(Lecturer lecturer);

        void UpdateLecturer(Lecturer lecturer);

        IEnumerable<Lecturer> GetAllLecturers(bool hasIncludeFacility, string keyword = null);

        Lecturer DeleteLecturerByModifyStatus(int id);

        Lecturer GetLecturerById(int id);

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

        public IEnumerable<Lecturer> GetAllLecturers(bool hasIncludeFacility, string keyword = null)
        {
            if (ExistingKeyWord())
                return !hasIncludeFacility ?
                    GetLecturersByKeywordNotIncludingFacility()
                    : GetLecturerByKeywordIncludingFacility();

            return !hasIncludeFacility
                ? _lecturerRepository.GetAll()
                .Where(n => n.Status).ToList()
                : _lecturerRepository.GetAll(new[] { CommonConstants.Facility })
                .Where(n => n.Status).ToList();

            bool ExistingKeyWord()
            {
                return !String.IsNullOrWhiteSpace(keyword);
            }

            List<Lecturer> GetLecturerByKeywordIncludingFacility()
            {
                return _lecturerRepository.GetMulti(n => n.Name.Contains(keyword)
                                                         || n.ID.ToString().Contains(keyword),
                        new[] { CommonConstants.Facility })
                    .Where(n => n.Status).ToList();
            }

            List<Lecturer> GetLecturersByKeywordNotIncludingFacility()
            {
                return _lecturerRepository.GetMulti(n => n.Name.Contains(keyword)
                                                         || n.ID.ToString().Contains(keyword))
                    .Where(n => n.Status).ToList();
            }
        }

        public Lecturer DeleteLecturerByModifyStatus(int id)
        {
            var lecturer = GetLecturerById(id);

            lecturer.Status = false;

            return lecturer;
        }

        public Lecturer GetLecturerById(int id)
        {
            return _lecturerRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}