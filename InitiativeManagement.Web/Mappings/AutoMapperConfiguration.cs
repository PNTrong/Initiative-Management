using AutoMapper;
using InitiativeManagement.Model.Models;
using InitiativeManagement.Web.Models;

namespace InitiativeManagement.Web.Mappings
{
    public class AutoMapperConfiguration :Profile
    {
        //public static void Configure()
        //{
        //    var config = new MapperConfiguration(cfg => {
        //        cfg.CreateMap<User, UserDto>();
        //    });
        //    Mapper.CreateMap<Post, PostViewModel>();
        //    Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
        //    Mapper.CreateMap<Tag, TagViewModel>();

        //    Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
        //    Mapper.CreateMap<Product, ProductViewModel>();
        //    Mapper.CreateMap<ProductTag, ProductTagViewModel>();
        //    Mapper.CreateMap<Footer, FooterViewModel>();
        //    Mapper.CreateMap<Slide, SlideViewModel>();
        //    Mapper.CreateMap<Page, PageViewModel>();
        //    Mapper.CreateMap<ContactDetail, ContactDetailViewModel>();

        //    Mapper.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
        //    Mapper.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
        //    Mapper.CreateMap<ApplicationUser, ApplicationUserViewModel>();
        //}

        protected override void Configure()
        {
            CreateMap<Post, PostViewModel>();
            CreateMap<PostCategory, PostCategoryViewModel>();
            CreateMap<Tag, TagViewModel>();

            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductTag, ProductTagViewModel>();
            CreateMap<Footer, FooterViewModel>();
            CreateMap<Slide, SlideViewModel>();
            CreateMap<Page, PageViewModel>();
            CreateMap<ContactDetail, ContactDetailViewModel>();

            CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
            CreateMap<ApplicationRole, ApplicationRoleViewModel>();
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
        }
    }
}