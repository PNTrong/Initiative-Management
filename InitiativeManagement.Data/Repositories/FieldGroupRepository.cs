using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Data.Repositories
{
    public interface IFieldGroupRepository : IRepository<FieldGroup>
    {
    }

    public class FieldGroupRepository : RepositoryBase<FieldGroup>, IFieldGroupRepository
    {
        public FieldGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}