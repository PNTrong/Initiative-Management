using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitiativeManagement.Model.Models
{
    [Table("AppraisalBoardCommnents")]
    public class AppraisalBoardCommnent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public string GeneralComment { set; get; }

        public string GPA { set; get; }

        public bool IsDeactive { set; get; }

        public virtual IEnumerable<AppraisalBoardMemberCommnent> AppraisalBoardMemberCommnents { set; get; }
    }
}