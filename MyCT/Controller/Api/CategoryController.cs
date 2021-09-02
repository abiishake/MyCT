using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MyCT.Core.Model.DTO;
using MyCT.Interface.BOObjects;
using MyCT.Interface.ServiceLocator;

namespace MyCT.Controller.Api
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        [Route("add")]
        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryDTO CategoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestMessage();
                }

                AddCreatedBy<CategoryDTO>(CategoryDTO);
                var CategoryBO = _serviceLocator.Resolve<ICategoryBO>();
                if (CategoryBO.Add(CategoryDTO, out _object) > 0)
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
        public IActionResult EditCategory([FromBody] CategoryDTO CategoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestMessage();
                }

                AddCreatedBy<CategoryDTO>(CategoryDTO);
                var CategoryBO = _serviceLocator.Resolve<ICategoryBO>();
                if (CategoryBO.Add(CategoryDTO, out _object) > 0)
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
                

                var CategoryBO = _serviceLocator.Resolve<ICategoryBO>();
                var list = CategoryBO.List();
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

        [HttpGet("{categoryid}")]
        public IActionResult GetById(int categoryid)
        {
            try
            {
                var CategoryBO = _serviceLocator.Resolve<ICategoryBO>();
                var category = CategoryBO.GetById(categoryid);
                if (category != null)
                {
                    return Ok(category);
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

        [HttpDelete("{categoryid}")]
        public IActionResult Delete(int id)
        {
            try
            { 
                var CategoryBO = _serviceLocator.Resolve<ICategoryBO>();
                if (CategoryBO.Remove(id))
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
