using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Models;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IFacilityRepository : IRepository<Facility>
    {
    }

    public class FacilityRepository : RepositoryBase<Facility>, IFacilityRepository
    {
        public FacilityRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}