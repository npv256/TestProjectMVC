using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityControl.Models
{
    public class ScienceDTO
    {
        [ForeignKey("TeacherDTO")]
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        [Required]
        public List<StudentCheckModel> Students { get; set; }

        public ScienceDTO()
        {
            Students = new List<StudentCheckModel>();
        }
    }
}