using MyCT.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Interface.BOObjects
{
    public interface ILatLongBO
    {
        int Add(LatLongDTO LatLongDTO);
        bool Remove(int id);
        int Edit(LatLongDTO LatLongDTO);
        List<LatLongDTO> List();
    }
}
