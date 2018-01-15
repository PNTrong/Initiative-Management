using System.Collections.Generic;

namespace InitiativeManagement.Web.Models
{
    public class ApplicationGroupViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { set; get; }

        public IEnumerable<ApplicationRoleViewModel> Roles { set; get; }
    }
}