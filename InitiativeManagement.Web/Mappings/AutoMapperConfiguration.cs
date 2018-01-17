using AutoMapper;
using InitiativeManagement.Model.Models;
using InitiativeManagement.Web.Models;

namespace InitiativeManagement.Web.Mappings
{
    public class AutoMapperConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
            CreateMap<ApplicationRole, ApplicationRoleViewModel>();
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
            CreateMap<InitiativeViewModel, Initiative>();
        }
    }
}