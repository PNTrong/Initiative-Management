using System;

namespace InitiativeManagement.Model.Models
{
    public class Author
    {
        public int Id { set; get; }

        public string FullName { set; get; }

        public string Address { set; get; }

        public DateTime BirthDay { set; get; }

        public string OfficeAddress { set; get; }

        public string Position { set; get; }

        public string Qualitification { set; get; }

        public string ContributionRate { set; get; }

        public string TrialApplicants { set; get; }

        public string AssistedtWorkContent { set; get; }

        public string OrganizationID { set; get; }
    }
}