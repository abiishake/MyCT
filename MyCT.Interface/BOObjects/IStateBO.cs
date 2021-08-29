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
        int Add(StateDTO StateDTO);
        bool Remove(int id);
        int Edit(StateDTO StateDTO);
        List<StateDTO> List();
    }
}
