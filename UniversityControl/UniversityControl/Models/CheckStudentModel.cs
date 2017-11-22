using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityControl.Models
{
    public class CheckStudentModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Checked { get ; set;}

        public CheckStudentModel()
        {
            Checked = false;
        }
    }
}