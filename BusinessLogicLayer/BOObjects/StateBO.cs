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
    public class StateBO : BaseBO, IStateBO
    {
        public StateBO(IServiceLocator serviceLocator) : base(serviceLocator)
        {

        }

        public int Add(StateDTO StateDTO, out Object _object)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    State state = new State()
                    {
                        Name = StateDTO.Name,
                        CreatedOn = StateDTO.CreatedOn
                    };

                    AddCreated<StateDTO, State>(StateDTO, state);
                    unitOfWork.States.Add(state);
                    _object = state;
                    return unitOfWork.Save();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public int Edit(StateDTO StateDTO, out object _object)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    State state = unitOfWork.States.GetById(StateDTO.Id);
                    state.Name = StateDTO.Name;
                    state.ModifiedOn = StateDTO.ModifiedOn;
                    _object = state;

                    AddModified<StateDTO, State>(StateDTO, state);
                    return unitOfWork.Save();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public StateDTO GetById(int id)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    State state = unitOfWork.States.GetById(id);
                    return new StateDTO()
                    {
                        Id = state.Id,
                        Name = state.Name
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<StateDTO> List()
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    List<StateDTO> States = unitOfWork.States.GetAll().Select(x => new StateDTO()
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList();

                    return States;
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
                    unitOfWork.States.RemoveById(id);
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
