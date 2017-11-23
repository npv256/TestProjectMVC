using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityControl.Models
{
    public class TeacherDTO 
    {
        [Key]
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ScienceDTO Science { get; set; }

        public TeacherDTO()
        {
            ScienceDTO Science = new ScienceDTO();
        }
    }
}