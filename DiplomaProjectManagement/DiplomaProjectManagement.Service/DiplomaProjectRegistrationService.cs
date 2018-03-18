using DiplomaProjectManagement.Common.CustomViewModel;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Model.Models;
using System.Linq;

namespace DiplomaProjectManagement.Service
{
    public interface IDiplomaProjectRegistrationService
    {
        DiplomaProjectRegistration RegisterDiplomaProject(DiplomaProjectRegistration diplomaProjectRegistration);

        bool IsExistsStudentRegistered(int studentId, int diplomaProjectId);

        DiplomaProjectRegistration AddDiplomaProjectRegistration(DiplomaProjectRegistration diplomaProjectRegistration);

        int GetNumberOfStudentRegistered(int diplomaProjectId, int registrationTimeId);

        DiplomaProjectDetailViewModel GetDiplomaProjectDetailByStudentId(int studentId);

        void Update(DiplomaProjectRegistration diplomaProjectRegistration);

        int FindDiplomaProject(int studentId, int registrationTimeId);

        DiplomaProjectRegistration FindDiplomaProjectRegistration(int studentId, int registrationTimeId,
            int diplomaProjectId);

        void UpdateTeamName(int currentStudentId, int partnerId, int diplomaProjectId,
            int registrationTimeId, string teamName);

        string FindTeamName(int studentId, int registrationTimeId);

        void Save();
    }

    public class DiplomaProjectRegistrationService : IDiplomaProjectRegistrationService
    {
        private readonly IDiplomaProjectRegistrationRepository _diplomaProjectRegistrationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DiplomaProjectRegistrationService(IDiplomaProjectRegistrationRepository diplomaProjectRegistrationRepository, IUnitOfWork unitOfWork)
        {
            _diplomaProjectRegistrationRepository = diplomaProjectRegistrationRepository;
            _unitOfWork = unitOfWork;
        }

        public bool IsExistsStudentRegistered(int studentId, int diplomaProjectId)
        {
            return _diplomaProjectRegistrationRepository.IsCurrentStudentRegistered(studentId, diplomaProjectId);
        }

        public DiplomaProjectRegistration AddDiplomaProjectRegistration(DiplomaProjectRegistration diplomaProjectRegistration)
        {
            return _diplomaProjectRegistrationRepository.Add(diplomaProjectRegistration);
        }

        public int GetNumberOfStudentRegistered(int diplomaProjectId, int registrationTimeId)
        {
            return _diplomaProjectRegistrationRepository
                .GetMulti(n => n.DiplomaProjectId == diplomaProjectId
                               && n.RegistrationTimeId == registrationTimeId)
                .Count();
        }

        public DiplomaProjectDetailViewModel GetDiplomaProjectDetailByStudentId(int studentId)
        {
            return _diplomaProjectRegistrationRepository
                .GetDiplomaProjectDetailByStudentId(studentId);
        }

        public void Update(DiplomaProjectRegistration diplomaProjectRegistration)
        {
            _diplomaProjectRegistrationRepository.Update(diplomaProjectRegistration);
        }

        public DiplomaProjectRegistration RegisterDiplomaProject(DiplomaProjectRegistration diplomaProjectRegistration)
        {
            return _diplomaProjectRegistrationRepository.Add(diplomaProjectRegistration);
        }

        public int FindDiplomaProject(int studentId, int registrationTimeId)
        {
            return _diplomaProjectRegistrationRepository.FindDiplomaProject(studentId, registrationTimeId);
        }

        public DiplomaProjectRegistration FindDiplomaProjectRegistration(int studentId, int registrationTimeId, int diplomaProjectId)
        {
            return _diplomaProjectRegistrationRepository
                .GetSingleByCondition(n => n.StudentId == studentId
                    && n.RegistrationTimeId == registrationTimeId
                    && n.DiplomaProjectId == diplomaProjectId);
        }

        public void UpdateTeamName(int currentStudentId, int partnerId, int diplomaProjectId,
            int registrationTimeId, string teamName)
        {
            var myRegistration = FindDiplomaProjectRegistration(currentStudentId, registrationTimeId, diplomaProjectId);
            myRegistration.TeamName = teamName;

            AddDiplomaProjectRegistration(new DiplomaProjectRegistration
            {
                StudentId = partnerId,
                DiplomaProjectId = diplomaProjectId,
                RegistrationTimeId = registrationTimeId,
                TeamName = teamName
            });
        }

        public string FindTeamName(int studentId, int registrationTimeId)
        {
            return _diplomaProjectRegistrationRepository.FindTeamName(studentId, registrationTimeId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}