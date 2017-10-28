namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinitiativev3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Initiatives", "AppraisalBoardCommnetId", "dbo.AppraisalBoardCommnents");
            DropIndex("dbo.Initiatives", new[] { "AppraisalBoardCommnetId" });
            AlterColumn("dbo.Initiatives", "AppraisalBoardCommnetId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Initiatives", "AppraisalBoardCommnetId", c => c.Int());
            CreateIndex("dbo.Initiatives", "AppraisalBoardCommnetId");
            AddForeignKey("dbo.Initiatives", "AppraisalBoardCommnetId", "dbo.AppraisalBoardCommnents", "Id");
        }
    }
}
