using MyCT.Core.Model.DTO;
using MyCT.Interface.BOObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCT.Core.Model.Entities;
using MyCT.Interface.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer.BOObjects
{
    public class CategoryBO : BaseBO, ICategoryBO
    {
        public CategoryBO(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public int Add(CategoryDTO CategoryDTO)
        {
            try
            {
                using (var unitOfWork = _serviceProvider.GetService<IUnitOfWork>())
                {
                    Category Category = new Category()
                    {
                        Name = CategoryDTO.Name
                    };
                    unitOfWork.Categories.Add(Category);
                    return unitOfWork.Save();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Edit(CategoryDTO CategoryDTO)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDTO> List()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
