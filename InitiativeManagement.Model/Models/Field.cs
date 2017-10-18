using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitiativeManagement.Model.Models
{
    [Table("Fields")]
    public class Field
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        public int FieldGroupId { set; get; }

        [ForeignKey("FieldGroupId")]
        public virtual FieldGroup FieldGroup { set; get; }

        public string FieldName { set; get; }
        public string IsDeactive { set; get; }
    }
}