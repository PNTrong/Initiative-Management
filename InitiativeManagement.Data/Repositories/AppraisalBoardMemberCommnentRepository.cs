using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Data.Repositories
{
    public interface IAppraisalBoardMemberCommnentRepository : IRepository<AppraisalBoardMemberCommnent>
    {
    }

    public class AppraisalBoardMemberCommnentRepository : RepositoryBase<AppraisalBoardMemberCommnent>, IAppraisalBoardMemberCommnentRepository
    {
        public AppraisalBoardMemberCommnentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}