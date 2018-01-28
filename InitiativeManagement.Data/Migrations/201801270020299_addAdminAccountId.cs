namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAdminAccountId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Initiatives", "AdminAccountId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Initiatives", "AdminAccountId");
        }
    }
}
