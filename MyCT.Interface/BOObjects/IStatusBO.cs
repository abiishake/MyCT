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
        int Add(StatusDTO StatusDTO, out Object _object);
        bool Remove(int id);
        int Edit(StatusDTO StatusDTO, out Object _object);
        StatusDTO GetById(int id);
        List<StatusDTO> List();
    }
}
