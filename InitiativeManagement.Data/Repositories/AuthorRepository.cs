using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Data.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
    }

    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}