using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitiativeManagement.Model.Models
{
    [Table("FieldGroups")]
    public class FieldGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public string Name { set; get; }

        public bool? IsDeactive { set; get; }

        public virtual IEnumerable<Field> Fields { set; get; }
    }
}