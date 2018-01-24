namespace DiplomaProjectManagement.Data.Infrastructures
{
    public class DbFactory : Disposable, IDbFactory
    {
        private DiplomaProjectDbContext _dbContext;

        public DiplomaProjectDbContext Init() => _dbContext ?? (_dbContext = new DiplomaProjectDbContext());

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}