using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCT.Core.Model.DTO;
using MyCT.Interface.BOObjects;
using MyCT.Interface.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCT.Controller.Api
{
    [Route("api/subsubcategory")]
    [ApiController]
    public class SubSubCategoryController : BaseController
    {
        public SubSubCategoryController(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        [Route("add")]
        [HttpPost]
        public IActionResult AddSubCategory([FromBody] SubCategoryDTO SubCategoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestMessage();
                }

                AddCreatedBy<SubCategoryDTO>(SubCategoryDTO);
                var SubCategoryBO = _serviceLocator.Resolve<ISubCategoryBO>();
                if (SubCategoryBO.Add(SubCategoryDTO, out _object) > 0)
                {
                    return Created(new Uri("", UriKind.Relative), _object);
                }
                else
                {
                    return BadRequestMessage("Error Occured");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("edit")]
        [HttpPut]
        public IActionResult EditSubCategory([FromBody] SubCategoryDTO SubCategoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestMessage();
                }

                AddCreatedBy<SubCategoryDTO>(SubCategoryDTO);
                var SubCategoryBO = _serviceLocator.Resolve<ISubCategoryBO>();
                if (SubCategoryBO.Add(SubCategoryDTO, out _object) > 0)
                {
                    return Ok(_object);
                }
                else
                {
                    return BadRequestMessage("Error Occured");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {


                var SubCategoryBO = _serviceLocator.Resolve<ISubCategoryBO>();
                var list = SubCategoryBO.List();
                if (list != null)
                {
                    return Ok(list);
                }
                else
                {
                    return BadRequestMessage("Error Occured");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("{subsubcategoryid}")]
        public IActionResult GetById(int subsubcategoryid)
        {
            try
            {
                var SubCategoryBO = _serviceLocator.Resolve<ISubCategoryBO>();
                var subcategory = SubCategoryBO.GetById(subsubcategoryid);
                if (subcategory != null)
                {
                    return Ok(subcategory);
                }
                else
                {
                    return BadRequestMessage("Error Occured");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("{subsubcategoryid}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var SubCategoryBO = _serviceLocator.Resolve<ISubCategoryBO>();
                if (SubCategoryBO.Remove(id))
                {
                    return NoContent();
                }
                else
                {
                    return BadRequestMessage("Error Occured");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
