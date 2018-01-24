using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace DiplomaProjectManagement.Service
{
    public interface IFacilityService
    {
        Facility AddFacility(Facility facility);

        void UpdateFacility(Facility facility);

        Facility DeleteFacility(int id);

        IEnumerable<Facility> GetAllFacilities();

        void Save();
    }

    public class FacilityService : IFacilityService
    {
        private readonly IFacilityRepository _facilityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FacilityService(IFacilityRepository facilityRepository, IUnitOfWork unitOfWork)
        {
            _facilityRepository = facilityRepository;
            _unitOfWork = unitOfWork;
        }

        public Facility AddFacility(Facility facility)
        {
            return _facilityRepository.Add(facility);
        }

        public void UpdateFacility(Facility facility)
        {
            _facilityRepository.Update(facility);
        }

        public Facility DeleteFacility(int id)
        {
            return _facilityRepository.Delete(id);
        }

        public IEnumerable<Facility> GetAllFacilities()
        {
            return _facilityRepository.GetAll().ToList();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}