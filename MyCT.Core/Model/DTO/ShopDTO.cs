using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Core.Model.DTO
{
    public class ShopDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string WebSite { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public string Logo { get; set; }
        public int OwnerId { get; set; }
        public int StatusId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
