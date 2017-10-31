using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace InitiativeManagement.Data.Repositories
{
    public interface IApplicationGroupRepository : IRepository<ApplicationGroup>
    {
        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);

        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);

        IEnumerable<ApplicationRole> GetRolesByUserId(string userId);

        bool IsAccountAdmin(string userId);
    }

    public class ApplicationGroupRepository : RepositoryBase<ApplicationGroup>, IApplicationGroupRepository
    {
        public ApplicationGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupId
                        where ug.UserId == userId
                        select g;
            return query;
        }

        public IEnumerable<ApplicationRole> GetRolesByUserId(string userId)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupId
                        where ug.UserId == userId
                        from r in DbContext.ApplicationRoles
                        join gr in DbContext.ApplicationRoleGroups
                        on r.Id equals gr.RoleId
                        where gr.GroupId == g.ID
                        select r;
            return query;
        }

        public bool IsAccountAdmin(string userId)
        {
            var query = from groupUser in DbContext.ApplicationUserGroups
                        where groupUser.UserId == userId && groupUser.GroupId == Common.CommonConstants.SupperAdminRole
                        select groupUser;
            var groupId = query.FirstOrDefault();
            if (groupId != null)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupId
                        join u in DbContext.Users
                        on ug.UserId equals u.Id
                        where ug.GroupId == groupId
                        select u;
            return query;
        }
    }
}