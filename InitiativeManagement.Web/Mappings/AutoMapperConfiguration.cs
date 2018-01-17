﻿using AutoMapper;
using InitiativeManagement.Model.Models;
using InitiativeManagement.Web.Models;

namespace InitiativeManagement.Web.Mappings
{
    public class AutoMapperConfiguration : Profile
    {
        protected override void Configure()
        {
            //CreateMap<Post, PostViewModel>();
            //CreateMap<PostCategory, PostCategoryViewModel>();
            //CreateMap<Tag, TagViewModel>();

            //CreateMap<ProductCategory, ProductCategoryViewModel>();
            //CreateMap<Product, ProductViewModel>();
            //CreateMap<ProductTag, ProductTagViewModel>();
            //CreateMap<Footer, FooterViewModel>();
            //CreateMap<Slide, SlideViewModel>();
            //CreateMap<Page, PageViewModel>();
            //CreateMap<ContactDetail, ContactDetailViewModel>();

            CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
            CreateMap<ApplicationRole, ApplicationRoleViewModel>();
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
            CreateMap<InitiativeViewModel, Initiative>();
        }
    }
}