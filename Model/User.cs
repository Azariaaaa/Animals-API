using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWorkshop.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
