namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinitiativev4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationGroups", "IsDeactive", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationRoles", "IsDeactive", c => c.Boolean());
            AddColumn("dbo.ApplicationUsers", "IsDeactive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Authors", "IsDeactive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Initiatives", "IsProvinceLevelApproved", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Initiatives", "ProvinceLevelGPA", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Initiatives", "ProvinceLevelGPA", c => c.String());
            AlterColumn("dbo.Initiatives", "IsProvinceLevelApproved", c => c.String());
            AlterColumn("dbo.Authors", "IsDeactive", c => c.Boolean());
            DropColumn("dbo.ApplicationUsers", "IsDeactive");
            DropColumn("dbo.ApplicationRoles", "IsDeactive");
            DropColumn("dbo.ApplicationGroups", "IsDeactive");
        }
    }
}
