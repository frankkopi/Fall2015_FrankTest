using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fall2015.Models;

namespace Fall2015.ViewModels
{
    public class CreateEditStudentViewModel
    {
        public Student Student { get; set; }
        public List<Education> Educations { get; set; } 
        public List<CompetencyHeader> CompetencyHeaders { get; set; }
    }

    
}










