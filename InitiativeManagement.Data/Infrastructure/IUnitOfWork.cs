namespace InitiativeManagement.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}