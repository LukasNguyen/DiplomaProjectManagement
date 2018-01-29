using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaProjectManagement.Data.Infrastructures;
using DiplomaProjectManagement.Model.Entities;

namespace DiplomaProjectManagement.Data.Repositories
{
    public interface IErrorRepository : IRepository<Error>
    {
    }
    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
