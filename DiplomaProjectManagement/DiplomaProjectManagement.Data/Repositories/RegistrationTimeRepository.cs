using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Enums;
using DiplomaProjectManagement.Model.Models;
using System.Linq;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IRegistrationTimeRepository : IRepository<RegistrationTime>
    {
        bool CheckExistingRegistrationTimeOpening();
    }

    public class RegistrationTimeRepository : RepositoryBase<RegistrationTime>, IRegistrationTimeRepository
    {
        public RegistrationTimeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public bool CheckExistingRegistrationTimeOpening()
        {
            return DbContext.RegistrationTimes.Any(n => n.RegistrationStatus == RegistrationStatus.Opening);
        }
    }
}