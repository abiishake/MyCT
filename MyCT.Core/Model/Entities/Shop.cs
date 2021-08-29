using MyCT.Core.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Core.Model.Entities
{
    public class Shop : ObjectDetails, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public virtual City City { get; set; }
        public int CityId { get; set; }
        public virtual State State { get; set; }
        public int StateId { get; set; }
        public string WebSite { get; set; }
        public LatLong LatLong { get; set; }
        public int LatLongId { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public string Logo { get; set; }
        public virtual CTUser Owner { get; set; }
        public int OwnerId { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }

    }
}
