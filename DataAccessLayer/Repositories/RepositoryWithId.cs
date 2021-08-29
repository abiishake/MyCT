
using MyCT.Core.Model.Interfaces;
using MyCT.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class RepositoryWithId<T> : Repository<T>, IRepositoryWithId<T> where T : class, IEntity
    {

        public RepositoryWithId(IServiceProvider serviceProvider)
           : base(serviceProvider)
        {

        }


        public T GetById(int id)
        {
            T entity = this._records.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public void RemoveById(int id)
        {
            this._records.Remove(GetById(id));
        }

    }
}
