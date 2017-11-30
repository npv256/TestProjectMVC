using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityControl.Models
{
    public class ScienceCheckModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}