using MyCT.Core.Model.Entities;
using MyCT.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ShopRepository : RepositoryWithId<Shop> ,IShopRepository
    {
        public ShopRepository(IServiceProvider serviceProvider) : base (serviceProvider)
        {

        }
    }
}
