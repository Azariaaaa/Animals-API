using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWorkshop.Model
{
    public class Race
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<Animal>? Animals { get; set; } = new List<Animal>();
    }
}
