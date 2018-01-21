using System;

namespace DiplomaProjectManagement.Data.Infrastructures
{
    public interface IDbFactory : IDisposable
    {
        DiplomaProjectDbContext Init();
    }
}