namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFieldAll : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Initiatives", name: "FielId", newName: "FieldId");
            RenameIndex(table: "dbo.Initiatives", name: "IX_FielId", newName: "IX_FieldId");
            AlterColumn("dbo.AppraisalBoardCommnents", "IsDeactive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AuthorGroups", "IsDeactive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Initiatives", "IsDeactive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Fields", "IsDeactive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.FieldGroups", "IsDeactive", c => c.Boolean(nullable: false));
            DropColumn("dbo.Initiatives", "IsBaselLevelApproved");
            DropColumn("dbo.Initiatives", "BaseLevelRank");
            DropColumn("dbo.Initiatives", "BaseLevelRantingGPA");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Initiatives", "BaseLevelRantingGPA", c => c.String());
            AddColumn("dbo.Initiatives", "BaseLevelRank", c => c.String());
            AddColumn("dbo.Initiatives", "IsBaselLevelApproved", c => c.String());
            AlterColumn("dbo.FieldGroups", "IsDeactive", c => c.Boolean());
            AlterColumn("dbo.Fields", "IsDeactive", c => c.Boolean());
            AlterColumn("dbo.Initiatives", "IsDeactive", c => c.Boolean());
            AlterColumn("dbo.AuthorGroups", "IsDeactive", c => c.Boolean());
            AlterColumn("dbo.AppraisalBoardCommnents", "IsDeactive", c => c.Boolean());
            RenameIndex(table: "dbo.Initiatives", name: "IX_FieldId", newName: "IX_FielId");
            RenameColumn(table: "dbo.Initiatives", name: "FieldId", newName: "FielId");
        }
    }
}
