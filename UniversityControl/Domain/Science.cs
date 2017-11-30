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
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }

        public Science()
        {
            Marks = new List<Mark>();
            Students = new List<Student>();
        }
    }
}
