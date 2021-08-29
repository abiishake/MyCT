using DataAccessLayer.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyCT.Interface.Repositories;
using MyCT.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyCTDbContext _myCTDbContext;
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();
        private IConfiguration Configuration;
        private IServiceProvider _serviceProvider;

        public UnitOfWork(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            this.Configuration = configuration;
            this._serviceProvider = serviceProvider;
        }

        public IShopRepository Shops
        {
            get { return GetRepository<IShopRepository>(); }
        }

        public ICategoryRepository Categories => GetRepository<ICategoryRepository>();

        public ISubCategoryRepository SubCategories => GetRepository<ISubCategoryRepository>();

        public IStateRepository States => GetRepository<IStateRepository>();

        public ICityRepository Cities => GetRepository<ICityRepository>();

        public IStatusRepository Status => GetRepository<IStatusRepository>();

        public ILatLongRepository LatLongs => GetRepository<ILatLongRepository>();

        private T GetRepository<T>() where T : class
        {
            Init();
            Type type = typeof(T);
            string name = type.IsGenericType ? type.GetGenericArguments()[0].Name : type.Name;
            object repositoryObject;
            if (!_cache.TryGetValue(name, out repositoryObject))
            {
                T repository = _serviceProvider.GetService<T>();
                repository.GetType().GetMethod("Init").Invoke(repository, null);
                _cache.Add(name, repository);
                return repository;
            }
            return (T)repositoryObject;
        }


       

        public void Init(string appsettings = "MyCTDbConnection")
        {
            if (_myCTDbContext == null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<MyCTDbContext>();
                optionsBuilder.UseMySQL(Configuration[$"ConnectionStrings:{appsettings}"]);
                _myCTDbContext = new MyCTDbContext(optionsBuilder.Options);
            }
        }

        public int Save()
        {
            Init();
            try
            {
                return _myCTDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            this.Init();
            return _myCTDbContext.Set<TEntity>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_myCTDbContext != null)
                    {
                        _myCTDbContext.Database.CloseConnection();
                        _myCTDbContext.Dispose();
                    }
                }
            }
            _disposed = true;
        }
    }
}
