namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_initiative_DateCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Initiatives", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Initiatives", "DateCreated");
        }
    }
}
