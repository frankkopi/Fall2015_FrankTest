using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fall2015.Models
{
    public class CompetencyHeader
    {
        public int CompetencyHeaderId { get; set; }
        public String Name { get; set; }

        public virtual ICollection<Competency> Competencies { get; set; } // navigation property
    }
}