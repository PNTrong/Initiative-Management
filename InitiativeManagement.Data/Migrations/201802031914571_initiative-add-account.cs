namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initiativeaddaccount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Initiatives", "AccountId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Initiatives", "AccountId");
            AddForeignKey("dbo.Initiatives", "AccountId", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Initiatives", "AccountId", "dbo.ApplicationUsers");
            DropIndex("dbo.Initiatives", new[] { "AccountId" });
            AlterColumn("dbo.Initiatives", "AccountId", c => c.String());
        }
    }
}
