using MyCT.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Interface.BOObjects
{
    public interface ISubCategoryBO
    {
        int Add(SubCategoryDTO SubCategoryDTO, out Object _object);
        bool Remove(int id);
        int Edit(SubCategoryDTO SubCategoryDTO, out Object _object);
        SubCategoryDTO GetById(int id);
        List<SubCategoryDTO> List();
    }
}
