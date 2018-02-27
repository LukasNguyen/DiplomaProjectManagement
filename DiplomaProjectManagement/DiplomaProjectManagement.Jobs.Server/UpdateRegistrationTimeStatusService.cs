using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;

namespace DiplomaProjectManagement.Jobs.Server
{
    public class UpdateRegistrationTimeStatusService
    {
        private readonly IRegistrationTimeRepository _registrationTimeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRegistrationTimeStatusService()
        {
            // this needs to be here, although this won't be used in the actual running
        }

        public UpdateRegistrationTimeStatusService(IRegistrationTimeRepository registrationTimeRepository, IUnitOfWork unitOfWork) : this()
        {
            _registrationTimeRepository = registrationTimeRepository;
            _unitOfWork = unitOfWork;
        }

        public void Run()
        {
            _registrationTimeRepository.UpdateRegistrationTimeStatusToTeacherAssignGrades();
            _unitOfWork.Commit();
        }
    }
}