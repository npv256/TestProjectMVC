using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Mark
    {
        public int Id { get; set; } 
        public long Key { get; set; }
        public double Value { get; set; }
        public int ScienceId { get; set; }
    }
}
