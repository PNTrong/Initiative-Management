using System;

namespace InitiativeManagement.Common
{
    public class DynamicFilter
    {
        public DynamicFilter()
        {
            Skip = 0;
            Take = 10;
            FieldId = -1;
            FieldGroupId = -1;
            AccountId = "";
            Keyword = "";
            StartDate = "";
            EndDate = "";
        }

        public int Skip { get; set; }

        public int Take { get; set; }

        public int? Order { get; set; }

        public string OrderBy { get; set; }

        public string Keyword { get; set; }

        public int? FieldId { get; set; }

        public string AccountId { get; set; }

        public int? FieldGroupId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}