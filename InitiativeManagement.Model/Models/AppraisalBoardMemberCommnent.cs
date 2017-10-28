using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitiativeManagement.Model.Models
{
    [Table("AppraisalBoardMemberCommnents")]
    public class AppraisalBoardMemberCommnent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required]
        public int AppraisalBoardCommnentId { set; get; }

        [ForeignKey("AppraisalBoardCommnentId")]
        public virtual AppraisalBoardCommnent AppraisalBoardCommnent { set; get; }

        public string Comment { set; get; }

        public string Point { set; get; }
        public bool IsDeactive { set; get; }
    }
}