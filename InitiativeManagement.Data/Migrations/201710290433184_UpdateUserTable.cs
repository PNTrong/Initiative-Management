namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "IsAccountAdmin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Initiatives", "UserId", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Initiatives", "UserId");
            DropColumn("dbo.ApplicationUsers", "IsAccountAdmin");
        }
    }
}