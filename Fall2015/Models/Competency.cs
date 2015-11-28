﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace Fall2015.Models
{
    public class Competency
    {
        public int CompetencyId { get; set; }
        public String Name { get; set; }
        public int CompetencyHeaderId { get; set; }

        public virtual CompetencyHeader CompetencyHeader { get; set; }

        // new code frank //part of the many-to-many relationsship between Student and Competency
        public virtual ICollection<Student> Students { get; set; } 


    }
}