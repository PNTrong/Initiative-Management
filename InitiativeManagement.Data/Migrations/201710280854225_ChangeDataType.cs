namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppraisalBoardMemberCommnents", "IsDeactive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Initiatives", "IsDeactive", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Initiatives", "IsDeactive", c => c.String());
            AlterColumn("dbo.AppraisalBoardMemberCommnents", "IsDeactive", c => c.Boolean());
        }
    }
}
