using System.Collections.Generic;
using System.Linq;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Model.Models;

namespace DiplomaProjectManagement.Service
{
    public interface IRegistrationTimeService
    {
        RegistrationTime AddRegistrationTime(RegistrationTime registrationTime);

        void UpdateRegistrationTime(RegistrationTime registrationTime);

        RegistrationTime DeleteRegistrationTime(int id);

        IEnumerable<RegistrationTime> GetAllRegistrationTimes();

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

        public RegistrationTime DeleteRegistrationTime(int id)
        {
            return _registrationTimeRepository.Delete(id);
        }

        public IEnumerable<RegistrationTime> GetAllRegistrationTimes()
        {
            return _registrationTimeRepository.GetAll().Where(rt => rt.Status).ToList();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}