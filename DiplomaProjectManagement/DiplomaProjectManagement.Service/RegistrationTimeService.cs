using System.Collections.Generic;
using System.Linq;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Model.Enums;
using DiplomaProjectManagement.Model.Models;

namespace DiplomaProjectManagement.Service
{
    public interface IRegistrationTimeService
    {
        RegistrationTime AddRegistrationTime(RegistrationTime registrationTime);

        void UpdateRegistrationTime(RegistrationTime registrationTime);

        RegistrationTime DeleteRegistrationTimeByModifyStatus(int id);

        IEnumerable<RegistrationTime> GetAllRegistrationTimes();

        RegistrationTime GetRegistrationTimeById(int id);


        void Save();
    }

    public class RegistrationTimeService : IRegistrationTimeService
    {
        private readonly IRegistrationTimeRepository _registrationTimeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationTimeService(IRegistrationTimeRepository registrationTimeRepository, IUnitOfWork unitOfWork)
        {
            _registrationTimeRepository = registrationTimeRepository;
            _unitOfWork = unitOfWork;
        }

        public RegistrationTime AddRegistrationTime(RegistrationTime registrationTime)
        {
            return _registrationTimeRepository.Add(registrationTime);
        }

        public void UpdateRegistrationTime(RegistrationTime registrationTime)
        {
            _registrationTimeRepository.Update(registrationTime);
        }

        public RegistrationTime DeleteRegistrationTimeByModifyStatus(int id)
        {
            var registrationTime = _registrationTimeRepository.GetSingleById(id);

            registrationTime.RegistrationStatus = RegistrationStatus.ClosedAssignGradesTime;

            return registrationTime;
        }

        public IEnumerable<RegistrationTime> GetAllRegistrationTimes()
        {
            return _registrationTimeRepository.GetAll().ToList();
        }

        public RegistrationTime GetRegistrationTimeById(int id)
        {
            return _registrationTimeRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}