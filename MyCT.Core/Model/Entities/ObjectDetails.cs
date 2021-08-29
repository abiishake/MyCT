using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Core.Model.Entities
{
   public abstract class ObjectDetails
    {
        public CTUser CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public CTUser ModifiedBy { get; set; }
        public int ModifiedById { get; set; }
    }
}
