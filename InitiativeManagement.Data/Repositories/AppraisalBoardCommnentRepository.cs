using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Data.Repositories
{
    public interface IAppraisalBoardCommnentRepository : IRepository<AppraisalBoardCommnent>
    {
    }

    public class AppraisalBoardCommnentRepository : RepositoryBase<AppraisalBoardCommnent>, IAppraisalBoardCommnentRepository
    {
        public AppraisalBoardCommnentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}