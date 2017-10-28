namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinitiativev2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Initiatives", "AppraisalBoardCommnetId", "dbo.AppraisalBoardCommnents");
            DropIndex("dbo.Initiatives", new[] { "AppraisalBoardCommnetId" });
            AlterColumn("dbo.Initiatives", "AppraisalBoardCommnetId", c => c.Int());
            CreateIndex("dbo.Initiatives", "AppraisalBoardCommnetId");
            AddForeignKey("dbo.Initiatives", "AppraisalBoardCommnetId", "dbo.AppraisalBoardCommnents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Initiatives", "AppraisalBoardCommnetId", "dbo.AppraisalBoardCommnents");
            DropIndex("dbo.Initiatives", new[] { "AppraisalBoardCommnetId" });
            AlterColumn("dbo.Initiatives", "AppraisalBoardCommnetId", c => c.Int(nullable: false));
            CreateIndex("dbo.Initiatives", "AppraisalBoardCommnetId");
            AddForeignKey("dbo.Initiatives", "AppraisalBoardCommnetId", "dbo.AppraisalBoardCommnents", "Id", cascadeDelete: true);
        }
    }
}
