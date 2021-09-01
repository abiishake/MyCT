using MyCT.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Interface.BOObjects
{
    public interface ICityBO
    {
        int Add(CityDTO CityDTO, out Object _object);
        bool Remove(int id);
        int Edit(CityDTO CityDTO, out Object _object);
        CityDTO GetById(int id);
        List<CityDTO> List();
    }
}
