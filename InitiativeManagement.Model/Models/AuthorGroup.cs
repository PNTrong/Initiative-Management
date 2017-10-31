using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitiativeManagement.Model.Models
{
    [Table("AuthorGroups")]
    public class AuthorGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public DateTime DateCreate { set; get; }

        //public virtual IEnumerable<Author> Authors { set; get; }

        public bool IsDeactive { set; get; }
    }
}