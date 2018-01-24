﻿using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Data.Repositories;
using DiplomaProjectManagement.Model.Models;

namespace DiplomaProjectManagement.Service
{
    public interface IDiplomaProjectRegistrationService
    {
        DiplomaProjectRegistration RegisterDiplomaProject(DiplomaProjectRegistration diplomaProjectRegistration);

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