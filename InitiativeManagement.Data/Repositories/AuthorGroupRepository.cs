using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Data.Repositories
{
    public interface IAuthorGroupRepository : IRepository<AuthorGroup>
    {
    }

    public class AuthorGroupRepository : RepositoryBase<AuthorGroup>, IAuthorGroupRepository
    {
        public AuthorGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}