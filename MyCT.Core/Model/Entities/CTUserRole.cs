using Microsoft.AspNetCore.Identity;
using MyCT.Core.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCT.Core.Model.Entities
{
    public class CTUserRole : IdentityUserRole<int>, IEntity
    {
        public int Id { get; set; }
        public virtual CTUser CTUser { get; set; }
        public virtual CTRole CTRole { get; set; }
    }
}
