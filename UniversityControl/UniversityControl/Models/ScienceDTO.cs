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
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public List<StudentDTO> Students { get; set; }
        public Dictionary<long, float> Rating { get; set; }

        public ScienceDTO()
        {
            Students = new List<StudentDTO>();
            Rating = new Dictionary<long, float>();
        }
    }
}