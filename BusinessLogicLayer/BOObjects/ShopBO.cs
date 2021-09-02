using MyCT.Core.Model.DTO;
using MyCT.Interface.BOObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MyCT.Interface.UnitOfWork;
using MyCT.Core.Model.Entities;
using MyCT.Interface.ServiceLocator;
using System.Linq;

namespace BusinessLogicLayer.BOObjects
{
    public class ShopBO : BaseBO, IShopBO
    {

        public ShopBO(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public int Add(ShopDTO ShopDTO, out Object _object)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    Shop shop = new Shop()
                    {
                        Name = ShopDTO.Name,
                        CategoryId = ShopDTO.CategoryId,
                        SubCategoryId = ShopDTO.SubCategoryId,
                        Address = ShopDTO.Address,
                        ZipCode = ShopDTO.ZipCode,
                        CityId = ShopDTO.CityId,
                        StateId = ShopDTO.StateId,
                        WebSite = ShopDTO.WebSite,
                        OpenTime = ShopDTO.OpenTime,
                        CloseTime = ShopDTO.CloseTime,
                        Logo = ShopDTO.Logo,
                        OwnerId = ShopDTO.OwnerId,
                        StatusId = ShopDTO.StatusId,
                        CreatedOn = ShopDTO.CreatedOn,
                        Latitude = ShopDTO.Latitude,
                        Longitude = ShopDTO.Longitude
                    };

                    AddCreated<ShopDTO, Shop>(ShopDTO, shop);
                    unitOfWork.Shops.Add(shop);
                    _object = shop;
                    return unitOfWork.Save();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public int Edit(ShopDTO ShopDTO, out object _object)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    Shop shop = unitOfWork.Shops.GetById(ShopDTO.Id);
                    shop.Name = ShopDTO.Name;
                    shop.CategoryId = ShopDTO.CategoryId;
                    shop.SubCategoryId = ShopDTO.SubCategoryId;
                    shop.Address = ShopDTO.Address;
                    shop.ZipCode = ShopDTO.ZipCode;
                    shop.CityId = ShopDTO.CityId;
                    shop.StateId = ShopDTO.StateId;
                    shop.WebSite = ShopDTO.WebSite;
                    shop.OpenTime = ShopDTO.OpenTime;
                    shop.CloseTime = ShopDTO.CloseTime;
                    shop.Logo = ShopDTO.Logo;
                    shop.OwnerId = ShopDTO.OwnerId;
                    shop.StatusId = ShopDTO.StatusId;
                    shop.ModifiedOn = ShopDTO.ModifiedOn;
                    shop.Latitude = ShopDTO.Latitude;
                    shop.Longitude = ShopDTO.Longitude;
                    _object = shop;

                    AddModified<ShopDTO, Shop>(ShopDTO, shop);
                    return unitOfWork.Save();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ShopDTO GetById(int id)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    Shop shop = unitOfWork.Shops.GetById(id);
                    return new ShopDTO()
                    {
                        Id = shop.Id,
                        Name = shop.Name
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ShopDTO> List()
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    List<ShopDTO> shops = unitOfWork.Shops.GetAll().Select(x => new ShopDTO()
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList();

                    return shops;
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
                    unitOfWork.Shops.RemoveById(id);
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
