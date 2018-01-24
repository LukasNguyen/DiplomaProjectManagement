using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace DiplomaProjectManagement.Service
{
    public interface IDiplomaProjectService
    {
        DiplomaProject AddDiplomaProject(DiplomaProject diplomaProject);

        void UpdateDiplomaProject(DiplomaProject diplomaProject);

        DiplomaProject DeleteDiplomaProject(int id);

        IEnumerable<DiplomaProject> GetAllDiplomaProjects();

        IEnumerable<DiplomaProject> GetDiplomaProjectsActive();

        DiplomaProject GetDiplomaProjectByStudentId(int id);

        void Save();
    }

    public class DiplomaProjectService : IDiplomaProjectService
    {
        private readonly IDiplomaProjectRepository _diplomaProjectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DiplomaProjectService(IDiplomaProjectRepository diplomaProjectRepository, IUnitOfWork unitOfWork)
        {
            _diplomaProjectRepository = diplomaProjectRepository;
            _unitOfWork = unitOfWork;
        }

        public DiplomaProject AddDiplomaProject(DiplomaProject diplomaProject)
        {
            return _diplomaProjectRepository.Add(diplomaProject);
        }

        public void UpdateDiplomaProject(DiplomaProject diplomaProject)
        {
            _diplomaProjectRepository.Update(diplomaProject);
        }

        public DiplomaProject DeleteDiplomaProject(int id)
        {
            return _diplomaProjectRepository.Delete(id);
        }

        public IEnumerable<DiplomaProject> GetAllDiplomaProjects()
        {
            return _diplomaProjectRepository.GetAll().ToList();
        }

        public IEnumerable<DiplomaProject> GetDiplomaProjectsActive()
        {
            return _diplomaProjectRepository.GetDiplomaProjectsActive();
        }

        public DiplomaProject GetDiplomaProjectByStudentId(int id)
        {
            return _diplomaProjectRepository.GetDiplomaProjectByStudentId(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}