using Microsoft.EntityFrameworkCore;
using MyCT.Interface.Repositories;
using MyCT.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCT.Core.Model.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly IServiceProvider _serviceProvider;
        protected IUnitOfWork _unitOfWork;
        protected DbSet<T> _records;

        public Repository(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public void Add(T entity)
        {
            this._records.Add(entity);
        }

        public void Dispose()
        {
            if (this._unitOfWork != null)
            {
                this._unitOfWork.Dispose();
                this._unitOfWork = null;
            }
        }

        public List<T> GetAll()
        {
            return this._records.ToList();
        }


        public void Init()
        {
            this._unitOfWork = this._serviceProvider.GetService<IUnitOfWork>();
            this._records = this._unitOfWork.Set<T>();
        }

        public IQueryable<T> Records()
        {
            return this._records;
        }
    }
}
