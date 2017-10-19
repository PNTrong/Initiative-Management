namespace InitiativeManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppraisalBoardCommnents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GeneralComment = c.String(),
                        GPA = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppraisalBoardMemberCommnents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppraisalBoardCommnentId = c.Int(nullable: false),
                        Comment = c.String(),
                        Point = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppraisalBoardCommnents", t => t.AppraisalBoardCommnentId, cascadeDelete: true)
                .Index(t => t.AppraisalBoardCommnentId);
            
            CreateTable(
                "dbo.AuthorGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeactive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorGroupId = c.Int(nullable: false),
                        FullName = c.String(),
                        Address = c.String(),
                        BirthDay = c.DateTime(nullable: false),
                        OfficeAddress = c.String(),
                        Position = c.String(),
                        Qualitification = c.String(),
                        ContributionRate = c.String(),
                        TrialApplicants = c.String(),
                        AssistedtWorkContent = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        OrganizationID = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuthorGroups", t => t.AuthorGroupId, cascadeDelete: true)
                .Index(t => t.AuthorGroupId);
            
            CreateTable(
                "dbo.FieldGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeactive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldGroupId = c.Int(nullable: false),
                        FieldName = c.String(),
                        IsDeactive = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FieldGroups", t => t.FieldGroupId, cascadeDelete: true)
                .Index(t => t.FieldGroupId);
            
            CreateTable(
                "dbo.Initiatives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FielId = c.Int(nullable: false),
                        AuthorGroupId = c.Int(nullable: false),
                        AppraisalBoardCommnetId = c.Int(nullable: false),
                        Title = c.String(),
                        SendTo = c.String(),
                        Investor = c.String(),
                        DeploymentTime = c.DateTime(nullable: false),
                        KnowSolutionContent = c.String(),
                        ImprovedContent = c.String(),
                        ConditionApply = c.String(),
                        ActionSteps = c.String(),
                        InitiativeApplicability = c.String(),
                        SecurityInformation = c.String(),
                        AchievedByAuthors = c.String(),
                        AchievedByAnothers = c.String(),
                        IsBaselLevelApproved = c.String(),
                        IsProvinceLevelApproved = c.String(),
                        TrialApplicantIds = c.String(),
                        Attachments = c.String(),
                        ProvinceLevelRank = c.String(),
                        ProvinceLevelGPA = c.String(),
                        BaseLevelRank = c.String(),
                        BaseLevelRantingGPA = c.String(),
                        IsDeactive = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppraisalBoardCommnents", t => t.AppraisalBoardCommnetId, cascadeDelete: true)
                .ForeignKey("dbo.AuthorGroups", t => t.AuthorGroupId, cascadeDelete: true)
                .ForeignKey("dbo.Fields", t => t.FielId, cascadeDelete: true)
                .Index(t => t.FielId)
                .Index(t => t.AuthorGroupId)
                .Index(t => t.AppraisalBoardCommnetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Initiatives", "FielId", "dbo.Fields");
            DropForeignKey("dbo.Initiatives", "AuthorGroupId", "dbo.AuthorGroups");
            DropForeignKey("dbo.Initiatives", "AppraisalBoardCommnetId", "dbo.AppraisalBoardCommnents");
            DropForeignKey("dbo.Fields", "FieldGroupId", "dbo.FieldGroups");
            DropForeignKey("dbo.Authors", "AuthorGroupId", "dbo.AuthorGroups");
            DropForeignKey("dbo.AppraisalBoardMemberCommnents", "AppraisalBoardCommnentId", "dbo.AppraisalBoardCommnents");
            DropIndex("dbo.Initiatives", new[] { "AppraisalBoardCommnetId" });
            DropIndex("dbo.Initiatives", new[] { "AuthorGroupId" });
            DropIndex("dbo.Initiatives", new[] { "FielId" });
            DropIndex("dbo.Fields", new[] { "FieldGroupId" });
            DropIndex("dbo.Authors", new[] { "AuthorGroupId" });
            DropIndex("dbo.AppraisalBoardMemberCommnents", new[] { "AppraisalBoardCommnentId" });
            DropTable("dbo.Initiatives");
            DropTable("dbo.Fields");
            DropTable("dbo.FieldGroups");
            DropTable("dbo.Authors");
            DropTable("dbo.AuthorGroups");
            DropTable("dbo.AppraisalBoardMemberCommnents");
            DropTable("dbo.AppraisalBoardCommnents");
        }
    }
}
