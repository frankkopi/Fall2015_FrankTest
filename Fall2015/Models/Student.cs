using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Fall2015.Helpers;

namespace Fall2015.Models
{
    public class Student
    {
        public int StudentId { get; set; } // Id

        [Required(ErrorMessage = "Wrong, stupid user. You must have a firstname.")]
        public String Firstname { get; set; }

        [Required]
        public String Lastname { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        public String MobilePhone { get; set; }

        public String ProfileImagePath { get; set; }

        public String ApplicationUserId { get; set; } // A reference to a user in ApplicationUser table, the Id is a Guid (String)

        public int EducationId { get; set; } // Foreign Key


        //part of the many-to-many relationsship between Student and Competency
        public virtual ICollection<Competency> Competencies { get; set; }

        public virtual Education Education { get; set; } // navigation property

        public void SaveImage(HttpPostedFileBase image, String serverPath, String pathToFile)
        {
            if (image == null) return;

            string filename = Guid.NewGuid().ToString();
            ImageModel.ResizeAndSave(
                serverPath + pathToFile, filename,
                image.InputStream, 200 );

            ProfileImagePath = pathToFile + filename + ".jpg";
        }
    }
}