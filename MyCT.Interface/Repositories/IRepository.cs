using MyCT.Core.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Interface.Repositories
{
    public interface IRepository<T> : IDisposable where T : class, IEntity
    {
        void Init();
        IQueryable<T> Records();
        void Add(T entity);
        List<T> GetAll();
    }
}