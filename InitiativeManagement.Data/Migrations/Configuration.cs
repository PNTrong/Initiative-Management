namespace InitiativeManagement.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InitiativeManagement.Data.TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(InitiativeManagement.Data.TeduShopDbContext context)
        {
            CreateProductCategorySample(context);

            CreateSlide(context);
            //  This method will be called after migrating to the latest version.
            CreatePage(context);

            CreateContactDetail(context);

            CreateConfigTitle(context);

            CreateRole(context);
        }

        private void CreateConfigTitle(TeduShopDbContext context)
        {
            if (!context.SystemConfigs.Any(x => x.Code == "HomeTitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    ValueString = "Trang chủ TeduShop",
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    ValueString = "Trang chủ TeduShop",
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    ValueString = "Trang chủ TeduShop",
                });
            }
        }

        private void CreateRole(TeduShopDbContext context)
        {
            if (!context.ApplicationRoles.Any())
            {
                var roles = new List<ApplicationRole>()
                {
                    new ApplicationRole(){ Name = Role.CreateIntiniativeForAdmin, Description= "Có thể tạo và nộp đơn sáng kiến giúp cho các đơn vị khác.", IsDeactive = false},
                    new ApplicationRole(){ Name = Role.ViewIntiniativeForAdmin, Description= "Hiển thị danh mục sáng kiến cho sở Khoa Học và Công Nghệ.", IsDeactive = false},
                    new ApplicationRole(){ Name = Role.ViewIntiniativeForUser, Description= "Hiển thị danh mục sáng kiến", IsDeactive = false},
                    new ApplicationRole(){ Name = Role.CreateIntiniativeForUser, Description= "Nộp đơn sáng kiến", IsDeactive = false},

                    new ApplicationRole(){ Name = Role.ViewUser, Description= "Hiển thị danh mục tài khoản của các đơn vị.", IsDeactive = false},
                    new ApplicationRole(){ Name = Role.CreateUser, Description= "Tạo mới tài khoản cho một đơn vị.", IsDeactive = false},
                    new ApplicationRole(){ Name = Role.EditUser, Description= "Cập nhập tài khoản cho một đơn vị.", IsDeactive = false},
                    new ApplicationRole(){ Name = Role.DeleteUser, Description= "Xóa tài khoản", IsDeactive = false},

                    new ApplicationRole(){ Name = Role.ViewField, Description= "Hiển thị danh mục Lĩnh Vực", IsDeactive = false},
                    new ApplicationRole(){ Name = Role.CreateField, Description= "Tạo nhóm Lĩnh Vực", IsDeactive = false},
                    new ApplicationRole(){ Name = Role.EditField, Description= "Cập nhập Lĩnh Vực", IsDeactive = false},
                    new ApplicationRole(){ Name = Role.DeleteField, Description= "Xóa Lĩnh Vực", IsDeactive = false},

                    new ApplicationRole(){ Name = Role.ViewFieldGroup, Description= "Hiển thị danh mục nhóm Lĩnh Vực", IsDeactive = false},
                    new ApplicationRole(){ Name = Role.CreateFieldGroup, Description= "Tạo nhóm Lĩnh Vự.", IsDeactive = false},
                    new ApplicationRole(){ Name = Role.EditFieldGroup, Description= "Cập nhập nhóm Lĩnh Vực", IsDeactive = false},
                    new ApplicationRole(){ Name = Role.DeleteFieldGroup, Description= "Xóa nhóm Lĩnh Vực", IsDeactive = false},
                };

                context.ApplicationRoles.AddRange(roles.Distinct());

                context.SaveChanges();
            }

            if (!context.ApplicationGroups.Any())
            {
                context.ApplicationGroups.AddRange(new List<ApplicationGroup>()
                {
                    new ApplicationGroup(){ Name = Role.SupperAdmin, Description = "Quản trị viên", IsDeactive = false},
                    new ApplicationGroup(){ Name = Role.Admin, Description = "Sở Khoa Học và Công Nghệ", IsDeactive = false},
                    new ApplicationGroup(){ Name = Role.User, Description = "Đơn vị", IsDeactive = false},
                });
                context.SaveChanges();
            }
        }

        private void CreateProductCategorySample(InitiativeManagement.Data.TeduShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name="Điện lạnh",Alias="dien-lanh",Status=true },
                 new ProductCategory() { Name="Viễn thông",Alias="vien-thong",Status=true },
                  new ProductCategory() { Name="Đồ gia dụng",Alias="do-gia-dung",Status=true },
                   new ProductCategory() { Name="Mỹ phẩm",Alias="my-pham",Status=true }
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }

        private void CreateFooter(TeduShopDbContext context)
        {
            if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "";
            }
        }

        private void CreateSlide(TeduShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide() {
                        Name ="Slide 1",
                        DisplayOrder =1,
                        Status =true,
                        Url ="#",
                        Image ="/Assets/client/images/bag.jpg",
                        Content =@"	<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur
                            adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                        <span class=""on-get"">GET NOW</span>" },
                    new Slide() {
                        Name ="Slide 2",
                        DisplayOrder =2,
                        Status =true,
                        Url ="#",
                        Image ="/Assets/client/images/bag1.jpg",
                    Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>

                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >

                                <span class=""on-get"">GET NOW</span>"},
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreatePage(TeduShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                try
                {
                    var page = new Page()
                    {
                        Name = "Giới thiệu",
                        Alias = "gioi-thieu",
                        Content = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium ",
                        Status = true
                    };
                    context.Pages.Add(page);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
        }

        private void CreateContactDetail(TeduShopDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                try
                {
                    var contactDetail = new InitiativeManagement.Model.Models.ContactDetail()
                    {
                        Name = "Shop thời trang TEDU",
                        Address = "Ngõ 77 Xuân La",
                        Email = "tedu@gmail.com",
                        Lat = 21.0633645,
                        Lng = 105.8053274,
                        Phone = "095423233",
                        Website = "http://tedu.com.vn",
                        Other = "",
                        Status = true
                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
        }
    }
}