namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_initiative : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Initiatives", "FieldGroupId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Initiatives", "FieldGroupId");
        }
    }
}
