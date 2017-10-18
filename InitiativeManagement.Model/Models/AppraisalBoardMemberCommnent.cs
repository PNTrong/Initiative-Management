using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitiativeManagement.Model.Models
{
    [Table("AppraisalBoardMemberCommnents")]
    public class AppraisalBoardMemberCommnent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string MemberId { set; get; }

        [Required]
        public string AppraisalBoardCommnentId { set; get; }

        [ForeignKey("AppraisalBoardCommnentId")]
        public virtual AppraisalBoardCommnent AppraisalBoardCommnent { set; get; }

        public string Comment { set; get; }

        public string Point { set; get; }
    }
}