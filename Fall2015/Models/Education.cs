using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fall2015.Models
{
    public class Education
    {
        public int EducationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }

        // one to many relationship from Education to Student
        public virtual ICollection<Student> Students { get; set; } // navigation property
    }
}