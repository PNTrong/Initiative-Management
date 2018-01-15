using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InitiativeManagement.Web.Models
{
    public class GridModel<T>
    {
        public IEnumerable<T> items { set; get; }
        public int totalCount { set; get; }
    }
}