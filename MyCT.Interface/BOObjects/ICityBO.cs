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
        int Add(CityDTO CityDTO);
        bool Remove(int id);
        int Edit(CityDTO CityDTO);
        List<CityDTO> List();
    }
}
