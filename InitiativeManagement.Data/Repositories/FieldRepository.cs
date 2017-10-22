using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Data.Repositories
{
    public interface IFieldRepository : IRepository<Field>
    {
    }

    public class FieldRepository : RepositoryBase<Field>, IFieldRepository
    {
        public FieldRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}