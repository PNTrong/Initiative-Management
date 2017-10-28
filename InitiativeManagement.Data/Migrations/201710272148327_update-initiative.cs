namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinitiative : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Authors", "AuthorGroupId", "dbo.AuthorGroups");
            DropForeignKey("dbo.Initiatives", "AuthorGroupId", "dbo.AuthorGroups");
            DropIndex("dbo.Authors", new[] { "AuthorGroupId" });
            DropIndex("dbo.Initiatives", new[] { "AuthorGroupId" });
            AddColumn("dbo.Authors", "InitiativeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Authors", "InitiativeId");
            AddForeignKey("dbo.Authors", "InitiativeId", "dbo.Initiatives", "Id", cascadeDelete: true);
            DropColumn("dbo.Authors", "AuthorGroupId");
            DropColumn("dbo.Initiatives", "AuthorGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Initiatives", "AuthorGroupId", c => c.Int(nullable: false));
            AddColumn("dbo.Authors", "AuthorGroupId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Authors", "InitiativeId", "dbo.Initiatives");
            DropIndex("dbo.Authors", new[] { "InitiativeId" });
            DropColumn("dbo.Authors", "InitiativeId");
            CreateIndex("dbo.Initiatives", "AuthorGroupId");
            CreateIndex("dbo.Authors", "AuthorGroupId");
            AddForeignKey("dbo.Initiatives", "AuthorGroupId", "dbo.AuthorGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Authors", "AuthorGroupId", "dbo.AuthorGroups", "Id", cascadeDelete: true);
        }
    }
}
