using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
   public class Science
    {
        [ForeignKey("Teacher")]
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
       // public long? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }
        public Dictionary<long, float> Rating { get; set; }

        public Science()
        {
            Students = new List<Student>();
            Rating = new Dictionary<long, float>();
        }
    }
}
