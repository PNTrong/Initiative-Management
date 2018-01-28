using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitiativeManagement.Model.Models
{
    public class AuthorGroup
    {
        public int Id { set; get; }

        public DateTime DateCreate { set; get; }

        public bool IsDeactive { set; get; }
    }
}