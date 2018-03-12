using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Model.Models;
using System.Linq;
using DiplomaProjectManagement.Common.CustomViewModel;

namespace DiplomaProjectManagement.Service
{
    public interface IDiplomaProjectRegistrationService
    {
        DiplomaProjectRegistration RegisterDiplomaProject(DiplomaProjectRegistration diplomaProjectRegistration);

        bool IsExistsStudentRegistered(int studentId, int diplomaProjectId);

        DiplomaProjectRegistration AddDiplomaProjectRegistration(DiplomaProjectRegistration diplomaProjectRegistration);

        int GetNumberOfStudentRegistered(int diplomaProjectId, int registrationTimeId);

        DiplomaProjectDetailViewModel GetDiplomaProjectDetailByStudentId(int studentId);

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

        public DiplomaProjectRegistration RegisterDiplomaProject(DiplomaProjectRegistration diplomaProjectRegistration)
        {
            return _diplomaProjectRegistrationRepository.Add(diplomaProjectRegistration);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}