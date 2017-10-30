namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInitiativeType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Initiatives", "AccountId", c => c.String());
            DropColumn("dbo.Initiatives", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Initiatives", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Initiatives", "AccountId");
        }
    }
}
