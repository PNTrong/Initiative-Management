namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedeactivefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppraisalBoardCommnents", "IsDeactive", c => c.Boolean());
            AddColumn("dbo.AppraisalBoardMemberCommnents", "IsDeactive", c => c.Boolean());
            AddColumn("dbo.Authors", "IsDeactive", c => c.Boolean());
            AlterColumn("dbo.Fields", "IsDeactive", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fields", "IsDeactive", c => c.String());
            DropColumn("dbo.Authors", "IsDeactive");
            DropColumn("dbo.AppraisalBoardMemberCommnents", "IsDeactive");
            DropColumn("dbo.AppraisalBoardCommnents", "IsDeactive");
        }
    }
}
