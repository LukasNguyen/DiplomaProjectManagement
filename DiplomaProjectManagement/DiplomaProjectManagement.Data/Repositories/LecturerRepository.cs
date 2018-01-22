using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
