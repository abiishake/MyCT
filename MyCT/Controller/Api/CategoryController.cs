using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MyCT.Core.Model.DTO;
using MyCT.Interface.BOObjects;

namespace MyCT.Controller.Api
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [Route("add")]
        [HttpPost]
        public IActionResult PostAddCategory([FromBody] CategoryDTO CategoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }

                var CategoryBO = _serviceProvider.GetService<ICategoryBO>();
                if (CategoryBO.Add(CategoryDTO) > 0)
                {
                    return Ok("Category Added Successfully");
                }
                else
                {
                    return BadRequest("Error Occured");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
