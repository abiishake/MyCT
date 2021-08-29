using MyCT.Core.Model.Entities;
using MyCT.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class SubCategoryRepository : RepositoryWithId<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
