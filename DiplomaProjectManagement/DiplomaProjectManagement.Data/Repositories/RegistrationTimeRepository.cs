using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Models;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IRegistrationTimeRepository : IRepository<RegistrationTime>
    {
    }

    public class RegistrationTimeRepository : RepositoryBase<RegistrationTime>, IRegistrationTimeRepository
    {
        public RegistrationTimeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}