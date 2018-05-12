using DiplomaProjectManagement.Common;
using DiplomaProjectManagement.Common.CustomViewModel;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DiplomaProjectManagement.Service
{
    public interface IDiplomaProjectService
    {
        DiplomaProject AddDiplomaProject(DiplomaProject diplomaProject);

        void UpdateDiplomaProject(DiplomaProject diplomaProject);

        DiplomaProject DeleteDiplomaProjectByModifyStatus(int id);

        IEnumerable<DiplomaProject> GetAllDiplomaProjectsActive();

        IEnumerable<DiplomaProject> GetDiplomaProjectsByLecturerId(int lectureId, string keyword = null);

        IEnumerable<DiplomaProject> GetDiplomaProjectsToRegister(int studentId, string keyword = null);

        DiplomaProject GetDiplomaProjectById(int id);

        DiplomaProject GetDiplomaProjectByStudentId(int id);

        IEnumerable<DiplomaProjectRemainingSlotViewModel> GetDiplomaProjectsRemainingSlot(string keyword = null);

        void Save();
    }

    public class DiplomaProjectService : IDiplomaProjectService
    {
        private readonly IDiplomaProjectRepository _diplomaProjectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDiplomaProjectRegistrationRepository _diplomaProjectRegistrationRepository;
        private readonly IRegistrationTimeRepository _registrationTimeRepository;

        public DiplomaProjectService(
            IDiplomaProjectRepository diplomaProjectRepository,
            IDiplomaProjectRegistrationRepository diplomaProjectRegistrationRepository,
            IRegistrationTimeRepository registrationTimeRepository,
            IUnitOfWork unitOfWork)
        {
            _diplomaProjectRepository = diplomaProjectRepository;
            _diplomaProjectRegistrationRepository = diplomaProjectRegistrationRepository;
            _registrationTimeRepository = registrationTimeRepository;
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
            return _diplomaProjectRepository.GetAll().Where(n => n.Status).ToList();
        }

        public IEnumerable<DiplomaProject> GetDiplomaProjectsByLecturerId(int lectureId, string keyword = null)
        {
            var query = _diplomaProjectRepository.GetDiplomaProjectsByLecturerId(lectureId);
            if (!string.IsNullOrWhiteSpace(keyword))
                return query.Where(n => n.Name.Contains(keyword) || n.Description.Contains(keyword)).ToList();
            return query.ToList();
        }

        public IEnumerable<DiplomaProject> GetDiplomaProjectsToRegister(int studentId, string keyword = null)
        {
            var activeRegisterTimeId = GetRegisterTimeActiveId();
            if (activeRegisterTimeId == 0)
            {
                return Enumerable.Empty<DiplomaProject>();
            }

            var isStudentRegistered = _diplomaProjectRegistrationRepository
                .IsCurrentStudentRegistered(studentId, activeRegisterTimeId);

            if (isStudentRegistered)
            {
                return Enumerable.Empty<DiplomaProject>();
            }

            var query = _diplomaProjectRepository.GetAll().Include(n => n.Lecturer)
                .Where(n => n.IsDisplayed && n.Status);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var diplomaProjectsSearching = query.Where(n => n.Name.Contains(keyword)
                                        || n.Description.Contains(keyword)
                                        || n.Lecturer.Name.Contains(keyword))
                    .ToList();

                return CheckRemoveDiplomaProjectExceedNumberRegistered(activeRegisterTimeId, diplomaProjectsSearching);
            }

            var diplomaProjects = query.ToList();
            return CheckRemoveDiplomaProjectExceedNumberRegistered(activeRegisterTimeId, diplomaProjects);
        }

        private List<DiplomaProject> CheckRemoveDiplomaProjectExceedNumberRegistered(int activeRegisterTimeId, List<DiplomaProject> srcDiplomaProjects)
        {
            if (srcDiplomaProjects.Any())
            {
                var destDiplomaProjects = srcDiplomaProjects.ToList();
                foreach (var diplomaProject in srcDiplomaProjects)
                {
                    var numberOfStudentRegistered = _diplomaProjectRegistrationRepository
                        .GetMulti(n => n.DiplomaProjectId == diplomaProject.ID
                                       && n.RegistrationTimeId == activeRegisterTimeId)
                        .Count(n => n.IsFirstStudentInTeamRegistered);

                    if (numberOfStudentRegistered == int.Parse(ConfigHelper.GetByKey("LimitNumberOfStudentRegistered")))
                    {
                        destDiplomaProjects.Remove(diplomaProject);
                    }
                }

                return destDiplomaProjects;
            }

            return srcDiplomaProjects;
        }

        private int GetRegisterTimeActiveId()
        {
            return _registrationTimeRepository.GetRegistrationTimeActiveId();
        }

        public DiplomaProject GetDiplomaProjectById(int id)
        {
            return _diplomaProjectRepository.GetSingleById(id);
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

        public IEnumerable<DiplomaProjectRemainingSlotViewModel> GetDiplomaProjectsRemainingSlot(string keyword = null)
        {
            var diplomaProjects = _diplomaProjectRepository.GetDiplomaProjectsRemainingSlot();

            if (string.IsNullOrEmpty(keyword))
            {
                return diplomaProjects;
            }
            else
            {
                return diplomaProjects.Where(
                    n => n.LecturerName.Contains(keyword) ||
                    n.DiplomaProjectName.Contains(keyword) ||
                    n.StudentName.Contains(keyword));
            }
        }
    }
}