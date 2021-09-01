
using MyCT.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Interface.BOObjects
{
    public interface IShopBO
    {
        int Add(ShopDTO ShopDTO, out Object _object);
        bool Remove(int id);
        int Edit(ShopDTO ShopDTO, out Object _object);
        ShopDTO GetById(int id);
        List<ShopDTO> List();
    }
}
