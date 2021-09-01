using MyCT.Core.Model.DTO;
using MyCT.Core.Model.Entities;
using MyCT.Interface.BOObjects;
using MyCT.Interface.ServiceLocator;
using MyCT.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BOObjects
{
    public class SubSubCategoryBO : BaseBO, ISubCategoryBO
    {
        public SubSubCategoryBO(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public int Add(SubCategoryDTO SubCategoryDTO, out Object _object)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    SubCategory subCategory = new SubCategory()
                    {
                        Name = SubCategoryDTO.Name,
                        CategoryId = SubCategoryDTO.CategoryId,
                        CreatedOn = SubCategoryDTO.CreatedOn
                    };
                    unitOfWork.SubCategories.Add(subCategory);
                    _object = subCategory;
                    return unitOfWork.Save();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public int Edit(SubCategoryDTO SubCategoryDTO, out object _object)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    SubCategory subcategory = unitOfWork.SubCategories.GetById(SubCategoryDTO.Id);
                    subcategory.Name = SubCategoryDTO.Name;
                    subcategory.CategoryId = SubCategoryDTO.CategoryId;
                    subcategory.ModifiedOn = SubCategoryDTO.ModifiedOn;
                    _object = subcategory;
                    return unitOfWork.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SubCategoryDTO GetById(int id)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    SubCategory subcategory = unitOfWork.SubCategories.GetById(id);
                    return new SubCategoryDTO()
                    {
                        Id = subcategory.Id,
                        Name = subcategory.Name
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SubCategoryDTO> List()
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    List<SubCategoryDTO> categories = unitOfWork.SubCategories.GetAll().Select(x => new SubCategoryDTO()
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
                    unitOfWork.SubCategories.RemoveById(id);
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
