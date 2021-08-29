using MyCT.Core.Model.DTO;
using MyCT.Interface.BOObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MyCT.Interface.UnitOfWork;
using MyCT.Core.Model.Entities;

namespace BusinessLogicLayer.BOObjects
{
    public class ShopBO : BaseBO, IShopBO
    {
        public ShopBO(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public int Add(ShopDTO shopDTO)
        {
            try
            {
                using (var unitOfWork = _serviceProvider.GetService<IUnitOfWork>())
                {
                    Shop shop = new Shop()
                    {
                        Name = shopDTO.Name
                    };
                    unitOfWork.Shops.Add(shop);
                    return unitOfWork.Save();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Edit(ShopDTO shopDTO)
        {
            throw new NotImplementedException();
        }

        public List<ShopDTO> List()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
