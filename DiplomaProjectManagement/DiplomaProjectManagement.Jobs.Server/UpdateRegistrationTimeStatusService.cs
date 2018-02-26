using DiplomaProjectManagement.Data.Repositories;

namespace DiplomaProjectManagement.Jobs.Server
{
    public class UpdateRegistrationTimeStatusService
    {
        private readonly IRegistrationTimeRepository _registrationTimeRepository;

        public UpdateRegistrationTimeStatusService()
        {
            // this needs to be here, although this won't be used in the actual running
        }

        public UpdateRegistrationTimeStatusService(IRegistrationTimeRepository registrationTimeRepository) : this()
        {
            this._registrationTimeRepository = registrationTimeRepository;
        }

        public void Run()
        {
            _registrationTimeRepository.UpdateRegistrationTimeStatusToTeacherAssignGrades();
        }
    }
}