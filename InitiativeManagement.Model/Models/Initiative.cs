using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitiativeManagement.Model.Models
{
    [Table("Initiatives")]
    public class Initiative
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required]
        public string FielId { set; get; }

        [ForeignKey("FielId")]
        public virtual Field Field { set; get; }

        [Required]
        public string AuthorGroupId { set; get; }

        [ForeignKey("AuthorGroupId")]
        public virtual AuthorGroup AuthorGroup { set; get; }

        public string AppraisalBoardCommnetId { set; get; }

        [ForeignKey("AppraisalBoardCommnetId")]
        public virtual AppraisalBoardCommnent AppraisalBoardCommnent { set; get; }

        public string Title { set; get; }
        public string SendTo { set; get; }
        public string Investor { set; get; }
        public DateTime DeploymentTime { set; get; }
        public string KnowSolutionContent { set; get; }
        public string ImprovedContent { set; get; }
        public string ConditionApply { set; get; }
        public string ActionSteps { set; get; }
        public string InitiativeApplicability { set; get; }
        public string SecurityInformation { set; get; }
        public string AchievedByAuthors { set; get; }
        public string AchievedByAnothers { set; get; }
        public string IsBaselLevelApproved { set; get; }
        public string IsProvinceLevelApproved { set; get; }
        public string TrialApplicantIds { set; get; }
        public string Attachments { set; get; }
        public string ProvinceLevelRank { set; get; }
        public string ProvinceLevelGPA { set; get; }
        public string BaseLevelRank { set; get; }
        public string BaseLevelRantingGPA { set; get; }
        public string IsDeactive { set; get; }
    }
}