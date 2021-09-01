using MyCT.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Interface.BOObjects
{
    public interface IStateBO
    {
        int Add(StateDTO StateDTO, out Object _object);
        bool Remove(int id);
        int Edit(StateDTO StateDTO, out Object _object);
        StateDTO GetById(int id);
        List<StateDTO> List();
    }
}
