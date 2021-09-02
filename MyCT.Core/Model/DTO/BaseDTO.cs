using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyCT.Core.Model.DTO
{
    public class BaseDTO
    {
        [JsonIgnore]
        public int CreatedById { get; set; }
        [JsonIgnore]
        public int ModifiedById { get; set; }
        [JsonIgnore]
        public DateTime CreatedOn { get; set; }
        [JsonIgnore]
        public DateTime ModifiedOn { get; set; } 
    }
}
