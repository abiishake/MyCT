using Microsoft.EntityFrameworkCore;
using MyCT.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Interface.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        void Init(string appsettings);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int Save();
        IShopRepository Shops { get; }
        ICategoryRepository Categories { get; }
        ISubCategoryRepository SubCategories { get; }
        IStateRepository States { get; }
        ICityRepository Cities { get; }
        IStatusRepository Status { get; }
        ILatLongRepository LatLongs { get; }
    }
}
