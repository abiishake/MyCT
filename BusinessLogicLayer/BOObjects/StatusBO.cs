using MyCT.Core.Model.DTO;
using MyCT.Core.Model.Entities;
using MyCT.Interface.BOObjects;
using MyCT.Interface.ServiceLocator;
using MyCT.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BOObjects
{
    public class StatusBO : BaseBO, IStatusBO
    {
        public StatusBO(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public int Add(StatusDTO StatusDTO, out Object _object)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    Status status = new Status()
                    {
                        Name = StatusDTO.Name,
                        CreatedOn = StatusDTO.CreatedOn
                    };
                    unitOfWork.Status.Add(status);
                    _object = status;
                    return unitOfWork.Save();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public int Edit(StatusDTO StatusDTO, out object _object)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    Status status = unitOfWork.Status.GetById(StatusDTO.Id);
                    status.Name = StatusDTO.Name;
                    status.ModifiedOn = StatusDTO.ModifiedOn;
                    _object = status;
                    return unitOfWork.Save();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public StatusDTO GetById(int id)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    Status status = unitOfWork.Status.GetById(id);
                    return new StatusDTO()
                    {
                        Id = status.Id,
                        Name = status.Name
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<StatusDTO> List()
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    List<StatusDTO> Status = unitOfWork.Status.GetAll().Select(x => new StatusDTO()
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList();

                    return Status;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public bool Remove(int id)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.Status.RemoveById(id);
                    return true;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
