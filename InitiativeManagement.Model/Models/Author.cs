using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitiativeManagement.Model.Models
{
    [Table("Authors")]
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        //[Required]
        //public int AuthorGroupId { set; get; }

        //[ForeignKey("AuthorGroupId")]
        //public virtual AuthorGroup AuthorGroup { set; get; }

        public virtual IEnumerable<Author> Authors { set; get; }

        [Required]
        public int InitiativeId { get; set; }

        [ForeignKey("InitiativeId")]
        public virtual Initiative Initiative { get; set; }

        public string FullName { set; get; }
        public string Address { set; get; }
        public DateTime BirthDay { set; get; }
        public string OfficeAddress { set; get; }
        public string Position { set; get; }
        public string Qualitification { set; get; }
        public string ContributionRate { set; get; }
        public string TrialApplicants { set; get; }
        public string AssistedtWorkContent { set; get; }
        public DateTime DateCreate { set; get; }
        public string OrganizationID { set; get; }
        public bool IsDeactive { set; get; }
    }
}