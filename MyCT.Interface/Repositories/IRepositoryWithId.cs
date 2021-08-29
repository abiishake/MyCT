using MyCT.Core.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Interface.Repositories
{
    public interface IRepositoryWithId<T> : IRepository<T> where T : class, IEntity
    {
        T GetById(int id);
        void RemoveById(int id);
    }
}
