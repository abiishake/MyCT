using MyCT.Core.Model.DTO;
using MyCT.Core.Model.Entities;
using MyCT.Interface.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BOObjects
{
    public class BaseBO
    {
        protected IServiceLocator _serviceLocator;
        public BaseBO(IServiceLocator serviceLocator)
        {
            this._serviceLocator = serviceLocator;
        }

        protected void AddCreated<T, TEntity>(T DTO, TEntity Entity) where T: BaseDTO where TEntity : ObjectDetails
        {
            ((ObjectDetails)Entity).CreatedById = ((BaseDTO)DTO).CreatedById;
            ((ObjectDetails)Entity).CreatedOn = ((BaseDTO)DTO).CreatedOn;
        }

        protected void AddModified<T, TEntity>(T DTO, TEntity Entity) where T : BaseDTO where TEntity : ObjectDetails
        {
            ((ObjectDetails)Entity).ModifiedById = ((BaseDTO)DTO).ModifiedById;
            ((ObjectDetails)Entity).ModifiedOn = ((BaseDTO)DTO).ModifiedOn;
        }

    }
}
