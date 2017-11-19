using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
   public class Science
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public List<Person> Students { get; set; }
        public Dictionary<string, float> Rating { get; set; }
    }
}
