using MyCT.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Interface.BOObjects
{
    public interface IStatusBO
    {
        int Add(StatusDTO StatusDTO);
        bool Remove(int id);
        int Edit(StatusDTO StatusDTO);
        List<StatusDTO> List();
    }
}
