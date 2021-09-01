using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Core.Model.DTO
{
    public class BaseDTO
    {
        public int CreatedById { get; set; }
        public int ModifiedById { get; set; }
        public DateTime CreatedOn { get; set; } 
        public DateTime ModifiedOn { get; set; } 
    }
}
