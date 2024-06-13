using FinalWorkshop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinalWorkshop.Model
{
    public class Animal
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RaceId { get; set; }
        [JsonIgnore]
        public Race Race { get; set; }

    }
}
