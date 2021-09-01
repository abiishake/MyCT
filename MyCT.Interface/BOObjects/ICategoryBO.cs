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
        int Add(CategoryDTO CategoryDTO, out Object _object);
        bool Remove(int id);
        int Edit(CategoryDTO CategoryDTO, out Object _object);
        CategoryDTO GetById(int id);
        List<CategoryDTO> List();
    }
}
