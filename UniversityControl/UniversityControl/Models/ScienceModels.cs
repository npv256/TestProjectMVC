using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityControl.Models
{
    public class ScienceModels
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public List<Person> Students { get; set; }
        public Dictionary<string, float> Rating { get; set; }
    }
}