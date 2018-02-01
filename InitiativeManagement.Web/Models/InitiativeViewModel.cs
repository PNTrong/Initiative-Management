using InitiativeManagement.Model.Models;
using System;
using System.Collections.Generic;

namespace InitiativeManagement.Web.Models
{
    public class InitiativeViewModel
    {
        public InitiativeViewModel()
        {
            Authors = new List<Author>();
        }

        public int Id { set; get; }

        public int FieldId { set; get; }

        public int FieldGroupId { set; get; }

        public Field Field { set; get; }

        public string AccountId { get; set; }

        public string AdminAccountId { get; set; }

        public List<Author> Authors { get; set; }

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