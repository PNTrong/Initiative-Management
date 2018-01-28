using System;
using System.Collections.Generic;
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
        public int FieldId { set; get; }

        [ForeignKey("FieldId")]
        public virtual Field Field { set; get; }

        public int FieldGroupId { get; set; }

        /// <summary>
        /// the account that create new a initiative from admin account
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// the admin account added a new initiative for sub-account ( for a specific account user)
        /// </summary>
        public string AdminAccountId { get; set; }

        public int AppraisalBoardCommnetId { set; get; }

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

        public bool IsProvinceLevelApproved { set; get; }

        public string TrialApplicantIds { set; get; }

        public string Attachments { set; get; }

        public string ProvinceLevelRank { set; get; }

        public double ProvinceLevelGPA { set; get; }

        public bool IsDeactive { set; get; }

        public DateTime DateCreated { set; get; }

        public List<Author> Authors { get; set; }
    }
}