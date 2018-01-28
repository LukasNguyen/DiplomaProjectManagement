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

        DiplomaProject DeleteDiplomaProjectByModifyStatus(int id);

        IEnumerable<DiplomaProject> GetAllDiplomaProjectsActive();

        IEnumerable<DiplomaProject> GetDiplomaProjectsActiveInRegisterTime();

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

        public IEnumerable<DiplomaProject> GetAllDiplomaProjectsActive()
        {
            return _diplomaProjectRepository.GetAll().Where(n=>n.Status).ToList();
        }

        public IEnumerable<DiplomaProject> GetDiplomaProjectsActiveInRegisterTime()
        {
            return _diplomaProjectRepository.GetDiplomaProjectsActiveInRegisterTime();
        }

        public DiplomaProject GetDiplomaProjectByStudentId(int id)
        {
            return _diplomaProjectRepository.GetDiplomaProjectByStudentId(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public DiplomaProject DeleteDiplomaProjectByModifyStatus(int id)
        {
            var diplomaProject = _diplomaProjectRepository.GetSingleById(id);
            diplomaProject.Status = false;

            return diplomaProject;
        }
    }
}