using MyCT.Core.Model.Entities;
using MyCT.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CityRepository : RepositoryWithId<City>, ICityRepository
    {
        public CityRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
