using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProjectManagement.Data.Infrastructures
{
    public class DbFactory : Disposable,IDbFactory
    {
        private DiplomaProjectDbContext dbContext;

        public DiplomaProjectDbContext Init()
        {
            return dbContext ?? (dbContext = new DiplomaProjectDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose(); ;
        }
    }
}
