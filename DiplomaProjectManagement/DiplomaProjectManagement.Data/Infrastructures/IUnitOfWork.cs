namespace DiplomaProjectManagement.Data.Infrastructures
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}