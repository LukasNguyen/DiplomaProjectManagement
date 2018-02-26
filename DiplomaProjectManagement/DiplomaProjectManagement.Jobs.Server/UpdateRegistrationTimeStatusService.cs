using DiplomaProjectManagement.Data.Repositories;

namespace DiplomaProjectManagement.Jobs.Server
{
    public class UpdateRegistrationTimeStatusService
    {
        private readonly IRegistrationTimeRepository _registrationTimeRepository;

        public UpdateRegistrationTimeStatusService(IRegistrationTimeRepository registrationTimeRepository)
        {
            this._registrationTimeRepository = registrationTimeRepository;
        }

        public void Run()
        {
            _registrationTimeRepository.UpdateRegistrationTimeStatusToTeacherAssignGrades();
        }
    }
}