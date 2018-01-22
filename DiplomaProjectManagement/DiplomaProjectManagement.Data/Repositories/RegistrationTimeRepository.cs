using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
