namespace InitiativeManagement.Data.Migrations
{
    using Common;
    using Model.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InitiativeManagement.Data.TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(InitiativeManagement.Data.TeduShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            CreateConfigTitle(context);

            CreateRole(context);
        }

        private void CreateConfigTitle(TeduShopDbContext context)
        {
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
    }
}