using MyCT.Core.Model.Entities;
using MyCT.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class StateRepository : RepositoryWithId<State>, IStateRepository
    {
        public StateRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
