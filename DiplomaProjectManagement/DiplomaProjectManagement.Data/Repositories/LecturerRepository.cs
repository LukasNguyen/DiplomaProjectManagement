using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Models;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface ILecturerRepository : IRepository<Lecturer>
    {
    }

    public class LecturerRepository : RepositoryBase<Lecturer>, ILecturerRepository
    {
        public LecturerRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}