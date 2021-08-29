using MyCT.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Interface.BOObjects
{
   public interface ICategoryBO
    {
        int Add(CategoryDTO CategoryDTO);
        bool Remove(int id);
        int Edit(CategoryDTO CategoryDTO);
        List<CategoryDTO> List();
    }
}
