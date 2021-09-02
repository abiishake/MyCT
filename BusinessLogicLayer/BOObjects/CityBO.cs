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
    public class CityBO : BaseBO, ICityBO
    {
        public CityBO(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public int Add(CityDTO CityDTO, out Object _object)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    City city = new City()
                    {
                        Name = CityDTO.Name,
                        StateId = CityDTO.StateId,
                        CreatedOn = CityDTO.CreatedOn
                    };

                    AddCreated<CityDTO, City>(CityDTO, city);
                    unitOfWork.Cities.Add(city);
                    _object = city;
                    return unitOfWork.Save();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public int Edit(CityDTO CityDTO, out object _object)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    City city = unitOfWork.Cities.GetById(CityDTO.Id);
                    city.Name = CityDTO.Name;
                    city.StateId = CityDTO.StateId;
                    city.ModifiedOn = CityDTO.ModifiedOn;
                    _object = city;

                    AddModified<CityDTO, City>(CityDTO, city);
                    return unitOfWork.Save();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CityDTO GetById(int id)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    City city = unitOfWork.Cities.GetById(id);
                    return new CityDTO()
                    {
                        Id = city.Id,
                        Name = city.Name
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CityDTO> List()
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    List<CityDTO> Cities = unitOfWork.Cities.GetAll().Select(x => new CityDTO()
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList();

                    return Cities;
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
                    unitOfWork.Cities.RemoveById(id);
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
