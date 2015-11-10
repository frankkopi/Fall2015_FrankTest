using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fall2015.Models;

namespace Fall2015.ViewModels
{
    public class StudentIndexViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<CompetencyHeader> CompetencyHeaders { get; set; }

    }
}