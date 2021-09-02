using MyCT.Core.Model.DTO;
using MyCT.Interface.BOObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCT.Core.Model.Entities;
using MyCT.Interface.UnitOfWork;
using MyCT.Interface.ServiceLocator;

namespace BusinessLogicLayer.BOObjects
{
    public class CategoryBO : BaseBO, ICategoryBO
    {
        public CategoryBO(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public int Add(CategoryDTO CategoryDTO, out Object _object)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    Category category = new Category()
                    {
                        Name = CategoryDTO.Name,
                        CreatedOn = CategoryDTO.CreatedOn
                    };

                    AddCreated<CategoryDTO, Category>(CategoryDTO, category);
                    unitOfWork.Categories.Add(category);
                    _object = category;
                    return unitOfWork.Save();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public int Edit(CategoryDTO CategoryDTO, out object _object)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    Category category = unitOfWork.Categories.GetById(CategoryDTO.Id);
                    category.Name = CategoryDTO.Name;
                    category.ModifiedOn = CategoryDTO.ModifiedOn;
                    _object = category;
                    AddModified<CategoryDTO, Category>(CategoryDTO, category);
                    return unitOfWork.Save();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CategoryDTO GetById(int id)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    Category category = unitOfWork.Categories.GetById(id);
                    return new CategoryDTO()
                    {
                        Id = category.Id,
                        Name = category.Name
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CategoryDTO> List()
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    List<CategoryDTO> categories = unitOfWork.Categories.GetAll().Select(x => new CategoryDTO()
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList();

                    return categories;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public bool Remove(int id)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.Categories.RemoveById(id);
                    return true;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
