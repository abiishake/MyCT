using MyCT.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Interface.BOObjects
{
    public interface ISubSubCategoryDTO
    {
        int Add(SubCategoryDTO SubCategoryDTO);
        bool Remove(int id);
        int Edit(SubCategoryDTO SubCategoryDTO);
        List<SubCategoryDTO> List();
    }
}
