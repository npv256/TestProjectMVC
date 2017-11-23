using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityControl.Models
{
    public class StudentDTO
    {
        [ForeignKey("ScienceDTO")]
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Checked { get ; set;}
        public Science Science { get; set; }

        public StudentDTO()
        {
            Checked = false;
        }
    }
}