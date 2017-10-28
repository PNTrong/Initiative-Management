using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Data.Repositories
{
    public interface IInitiativeRepository : IRepository<Initiative>
    {
    }

    public class InitiativeRepository : RepositoryBase<Initiative>, IInitiativeRepository
    {
        public InitiativeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}