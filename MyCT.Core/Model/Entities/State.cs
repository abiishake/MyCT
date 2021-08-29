using MyCT.Core.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Core.Model.Entities
{
    public class State : ObjectDetails, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}